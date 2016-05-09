using BL.Filters;
using BL.Metadata;
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
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media;

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
        private IList<Tuple<DateTime, double>> balances;
        private IList<TransactionCategoryList> transactionCategories;
        private BankAccountInfo bankAccountInfo;
        private ICommand syncCommand;
        private Timer syncExecuteTimer;
        private bool syncButtonForceDisabled;
        private string categoryOther;
        private string linearAxisInfo;
        private IList<SolidColorBrush> brushes;

        public IList<SolidColorBrush> Brushes { get { return brushes; } }

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
            private set
            {
                if (syncCommand == value)
                    return;

                syncCommand = value;
                NotifyPropertyChanged();
            }
        }

        public IList<TransactionCategoryList> TransactionCategories
        {
            get { return transactionCategories; }
            private set
            {
                if (transactionCategories == value)
                    return;

                transactionCategories = value;
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

        public IList<Tuple<DateTime, double>> Balances
        {
            get { return balances; }
            private set
            {
                if (balances == value)
                    return;

                balances = value;
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
                LinearAxisInfo = string.Format(resources.GetString("Bank_LinearAxisBalanceInfo"), bankAccountInfo?.Currency);
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
            brushes = new List<SolidColorBrush> { new SolidColorBrush(Colors.Black), new SolidColorBrush(Colors.DarkSlateGray) };
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
            NotifyPropertyChanged(nameof(TransactionCategories));

            if (Bank == null)
            {
                Balances = null;
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
            Balances = trans;

            TransactionCategories = await categoryService.GroupTransactions(Bank.StoredTransactions, categoryOther);
            Incomes = TransactionCategories.Where(x => x.Transactions.Any(y => y.Amount > 0)).Select(x => Tuple.Create(x.CategoryName, (double)x.Transactions.Where(y => y.Amount > 0).Sum(y => y.Amount))).ToList();
            Expenses = TransactionCategories.Where(x => x.Transactions.Any(y => y.Amount < 0)).Select(x => Tuple.Create(x.CategoryName, (double)x.Transactions.Where(y => y.Amount < 0).Sum(y => y.Amount))).ToList();
        }
    }
}
