using BL.Filters;
using BL.Models;
using BL.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using VirtualWallet.Controls;
using Windows.ApplicationModel.Resources;
using Windows.UI.Core;
using Windows.UI.Popups;

namespace VirtualWallet.ViewModels
{
    public class BankPageViewModel : ViewModelBase, IDisposable
    {
        private IBankAccountInfoService bankAccountInfoService;
        private ITransactionService transactionService;
        private ICategoryService categoryService;
        private ResourceLoader resources;
        private Bank bank;
        private IList<Tuple<string, double>> expenses;
        private IList<Tuple<string, double>> incomes;
        private IList<Tuple<DateTime, double>> transactions;
        private BankAccountInfo bankAccountInfo;
        private ICommand syncCommand;
        private Timer syncExecuteTimer;
        private bool syncButtonForceDisabled;
        private string categoryOther;
        private string linearAxisInfo;

        public Action BeforeSync { get; set; }

        public Action AfterSync { get; set; }

        public string LinearAxisInfo
        {
            get { return linearAxisInfo; }
            private set
            {
                if (linearAxisInfo == value)
                    return;

                linearAxisInfo = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand SyncCommand
        {
            get { return syncCommand; }
            set
            {
                if (syncCommand == value)
                    return;

                syncCommand = value;
                NotifyPropertyChanged();
            }
        }

        public Bank Bank
        {
            get { return bank; }
            private set
            {
                if (bank == value)
                    return;

                bank = value;
                NotifyPropertyChanged();

                BankAccountInfo = Bank?.BankAccountInfo ?? new BankAccountInfo();
                SyncCommand = new CommandHandler(SyncExecute, () => !syncButtonForceDisabled && (Bank?.CanSyncExecute ?? false));
                SetSyncExecuteTimer();
            }
        }

        public IList<Tuple<string, double>> Expenses
        {
            get { return expenses; }
            private set
            {
                if (expenses == value)
                    return;

                expenses = value;
                NotifyPropertyChanged();
            }
        }

        public IList<Tuple<string, double>> Incomes
        {
            get { return incomes; }
            private set
            {
                if (incomes == value)
                    return;

                incomes = value;
                NotifyPropertyChanged();
            }
        }

        public IList<Tuple<DateTime, double>> Transactions
        {
            get { return transactions; }
            private set
            {
                if (transactions == value)
                    return;

                transactions = value;
                NotifyPropertyChanged();
            }
        }

        public BankAccountInfo BankAccountInfo
        {
            get { return bankAccountInfo; }
            private set
            {
                if (bankAccountInfo == value)
                    return;

                bankAccountInfo = value;
                linearAxisInfo = string.Format(resources.GetString("Bank_LinearAxisBalanceInfo"), bankAccountInfo?.Currency);
                NotifyPropertyChanged();
            }
        }

        public BankPageViewModel(IBankAccountInfoService bankAccountInfoService, ITransactionService transactionService, ICategoryService categoryService, ResourceLoader resources)
        {
            this.bankAccountInfoService = bankAccountInfoService;
            this.transactionService = transactionService;
            this.categoryService = categoryService;
            this.resources = resources;
            categoryOther = resources.GetString("Category_Other");
        }

        public async Task LoadDataAsync(Bank bank)
        {
            if (bank != null)
            {
                bank.BankAccountInfo = await bankAccountInfoService.GetAsync(bank.Id);
                var filter = new Shared.Filters.TransactionFilter() { DateSince = DateTime.Now.AddMonths(-1) };
                bank.StoredTransactions = await transactionService.GetByBankIdAsync(bank.Id);
            }

            Bank = bank;
            await ReloadTransactions();
        }

        public void Dispose()
        {
            syncExecuteTimer?.Dispose();
        }

        private async void SyncExecute()
        {
            BeforeSync?.Invoke();

            // Disable button immediatly after invoking command
            syncButtonForceDisabled = true;
            NotifyPropertyChanged(nameof(SyncCommand));

            var filter = new TransactionFilter();
            filter.AddMonths(1);

            try
            {
                var bankTransactions = await Bank.GetTransactionsAsync(filter);
                BankAccountInfo = Bank.BankAccountInfo;

                if (Bank.StoredTransactions != null && Bank.StoredTransactions.Any())
                {
                    var toStore = new List<Transaction>();
                    foreach (var transaction in bankTransactions)
                    {
                        if (!Bank.StoredTransactions.Any(x => x.ExternalId == transaction.ExternalId))
                        {
                            Bank.StoredTransactions.Add(transaction);
                            toStore.Add(transaction);
                        }
                    }

                    await transactionService.InsertOrIgnoreAsync(false, toStore.ToArray());
                }
                else
                {
                    Bank.StoredTransactions = bankTransactions;
                    await transactionService.InsertOrIgnoreAsync(false, bankTransactions.ToArray());
                }

                var amount = BankAccountInfo.ClosingBalance;
                await ReloadTransactions();

                await bankAccountInfoService.InsertOrReplaceAsync(false, BankAccountInfo);
            }
            catch (Exception)
            {
                var dialog = new MessageDialog(resources.GetString("Bank_ErrorDialog"));
                dialog.Commands.Add(new UICommand(resources.GetString("Dialog_Close")));

                await dialog.ShowAsync();
            }

            syncButtonForceDisabled = false;
            NotifyPropertyChanged(nameof(SyncCommand));
            SetSyncExecuteTimer();

            AfterSync?.Invoke();
        }

        private void SetSyncExecuteTimer()
        {
            if (syncExecuteTimer != null)
                syncExecuteTimer.Dispose();

            if (!(Bank?.CanSyncExecute ?? true))
            {
                var dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;
                syncExecuteTimer = new Timer(async (obj) => await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => NotifyPropertyChanged(nameof(SyncCommand))), null, (int)Math.Ceiling((Bank.NextPossibleSyncTime - DateTime.Now).TotalMilliseconds), Timeout.Infinite);
            }
        }

        private async Task ReloadTransactions()
        {
            if (Bank == null)
            {
                Transactions = null;
                Incomes = null;
                Expenses = null;
                return;
            }

            var amount = BankAccountInfo.ClosingBalance;
            var trans = Bank.StoredTransactions.GroupBy(x => x.Date).Select(x => Tuple.Create(x.Key, x.Sum(y => y.Amount))).OrderByDescending(x => x.Item1).Select(x =>
            {
                amount -= (double)x.Item2;
                return Tuple.Create(x.Item1, amount);
            }).ToList();

            if (trans.FirstOrDefault()?.Item1.Date == DateTime.Now.Date)
                trans.RemoveAt(0);

            trans.Add(Tuple.Create(DateTime.Now, BankAccountInfo.ClosingBalance));
            Transactions = trans;

            var transactionCategories = await categoryService.GroupTransactions(Bank.StoredTransactions);
            Incomes = transactionCategories.Where(x => x.Item2 > 0).Select(x => Tuple.Create(x.Item1?.Name ?? categoryOther, (double)x.Item2)).ToList();
            Expenses = transactionCategories.Where(x => x.Item3 > 0).Select(x => Tuple.Create(x.Item1?.Name ?? categoryOther, (double)x.Item3)).ToList();
        }
    }
}
