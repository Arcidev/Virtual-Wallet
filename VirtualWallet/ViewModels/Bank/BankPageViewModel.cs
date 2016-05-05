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

        public Action BeforeSync { get; set; }

        public Action AfterSync { get; set; }

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
                Transactions = Bank?.StoredTransactions.Select(x => Tuple.Create(x.Date, (double)x.Amount)).ToList();
                SyncCommand = new CommandHandler(SyncExecute, () => !syncButtonForceDisabled && (Bank?.CanSyncExecute ?? false));
                SetSyncExecuteTimer();
            }
        }

        public IList<Tuple<string, double>> Expenses
        {
            get { return expenses; }
            private set
            {
                expenses = value;
                NotifyPropertyChanged();
            }
        }

        public IList<Tuple<string, double>> Incomes
        {
            get { return incomes; }
            private set
            {
                incomes = value;
                NotifyPropertyChanged();
            }
        }

        public IList<Tuple<DateTime, double>> Transactions
        {
            get { return transactions; }
            private set
            {
                transactions = value;
                NotifyPropertyChanged();
            }
        }

        public BankAccountInfo BankAccountInfo
        {
            get { return bankAccountInfo; }
            set
            {
                if (bankAccountInfo == value)
                    return;

                bankAccountInfo = value;
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
                bank.StoredTransactions = await transactionService.GetByBankIdAsync(bank.Id);
                var transactionCategories = await categoryService.GroupTransactions(bank.StoredTransactions);
                Incomes = transactionCategories.Where(x => x.Item2 > 0).Select(x => Tuple.Create(x.Item1?.Name ?? categoryOther, (double)x.Item2)).ToList();
                Expenses = transactionCategories.Where(x => x.Item3 > 0).Select(x => Tuple.Create(x.Item1?.Name ?? categoryOther, (double)x.Item3)).ToList();
            }

            Bank = bank;
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

                Transactions = Bank.StoredTransactions.Select(x => Tuple.Create(x.Date, (double)x.Amount)).ToList();
                var transactionCategories = await categoryService.GroupTransactions(bank.StoredTransactions);
                Incomes = transactionCategories.Where(x => x.Item2 > 0).Select(x => Tuple.Create(x.Item1?.Name ?? categoryOther, (double)x.Item2)).ToList();
                Expenses = transactionCategories.Where(x => x.Item3 > 0).Select(x => Tuple.Create(x.Item1?.Name ?? categoryOther, (double)x.Item3)).ToList();

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
    }
}
