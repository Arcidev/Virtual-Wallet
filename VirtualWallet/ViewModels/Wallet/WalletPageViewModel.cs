using BL.Filters;
using BL.Metadata;
using BL.Models;
using BL.Service;
using Shared.Filters;
using Shared.Formatters;
using Shared.Modifiers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class WalletPageViewModel : ViewModelBase, IDisposable
    {
        private IBankAccountInfoService bankAccountInfoService;
        private ITransactionService transactionService;
        private ICategoryService categoryService;
        private IWalletCategoryService walletCategoryService;
        private IWalletBankService walletBankService;
        private ResourceLoader resources;
        private Wallet wallet;
        private ObservableCollection<Bank> banks;
        private ObservableCollection<Category> categories;
        private IList<Tuple<string, double>> expenses;
        private IList<Tuple<string, double>> incomes;
        private IList<Tuple<DateTime, double>> balances;
        private IList<TransactionCategoryList> transactionCategories;
        private ICommand syncCommand;
        private Timer syncExecuteTimer;
        private bool syncButtonForceDisabled;
        private string categoryOther;
        private string linearAxisInfo;
        private IList<SolidColorBrush> brushes;
        private double closingBalance;
        private double openingBalance;
        private DateTime openingDate;
        private DateTime lastSync;
        private string currency;

        public IList<SolidColorBrush> Brushes { get { return brushes; } }

        public Action BeforeSync { get; set; }

        public Action AfterSync { get; set; }

        public bool HasTransactions { get { return TransactionCategories?.Any() ?? false; } }

        public bool HasIncomes { get { return Incomes?.Any() ?? false; } }

        public bool HasExpenses { get { return Expenses?.Any() ?? false; } }

        public bool HasBalances { get { return Balances?.Any() ?? false; } }

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
                NotifyPropertyChanged(nameof(HasTransactions));
            }
        }

        public Wallet Wallet
        {
            get { return wallet; }
            private set
            {
                if (wallet == value)
                    return;

                wallet = value;
                NotifyPropertyChanged();

                // BankAccountInfo = Bank?.BankAccountInfo ?? new BankAccountInfo();
            }
        }

        public double ClosingBalance
        {
            get { return closingBalance; }
            private set
            {
                if (closingBalance == value)
                    return;

                closingBalance = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(ClosingBalanceString));
            }
        }

        public string ClosingBalanceString
        {
            get { return CurrencyFormatter.Format(closingBalance, Currency); }
        }

        public double OpeningBalance
        {
            get { return openingBalance; }
            private set
            {
                if (openingBalance == value)
                    return;

                openingBalance = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(OpeningBalanceString));
            }
        }

        public string OpeningBalanceString
        {
            get { return CurrencyFormatter.Format(openingBalance, Currency); }
        }

        public DateTime OpeningDate
        {
            get { return openingDate; }
            private set
            {
                if (openingDate == value)
                    return;

                openingDate = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(OpeningDateString));
            }
        }

        public string OpeningDateString
        {
            get { return DateTimeFormatter.ToShortDate(OpeningDate); }
        }

        public DateTime LastSync
        {
            get { return lastSync; }
            private set
            {
                if (lastSync == value)
                    return;

                lastSync = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(LastSyncString));
            }
        }

        public string LastSyncString
        {
            get { return DateTimeFormatter.ToShortDate(LastSync); }
        }

        public string Currency
        {
            get { return currency; }
            private set
            {
                if (currency == value)
                    return;

                currency = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<Bank> Banks
        {
            get { return banks; }
            private set
            {
                if (banks == value)
                    return;

                banks = value;
                NotifyPropertyChanged();

                SyncCommand = new CommandHandler(SyncExecute, () => !syncButtonForceDisabled);
                SetSyncExecuteTimer();
            }
        }

        public ObservableCollection<Category> Categories
        {
            get { return categories; }
            private set
            {
                if (categories == value)
                    return;

                categories = value;
                NotifyPropertyChanged();
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
                NotifyPropertyChanged(nameof(HasExpenses));
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
                NotifyPropertyChanged(nameof(HasIncomes));
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
                NotifyPropertyChanged(nameof(HasBalances));
            }
        }

        private void setChartCurrency(String currency)
        {
            LinearAxisInfo = string.Format(resources.GetString("Bank_LinearAxisBalanceInfo"), currency);
        }

        public WalletPageViewModel(IBankAccountInfoService bankAccountInfoService, ITransactionService transactionService, ICategoryService categoryService, IWalletCategoryService walletCategoryService, IWalletBankService walletBankService, ResourceLoader resources)
        {
            this.bankAccountInfoService = bankAccountInfoService;
            this.transactionService = transactionService;
            this.categoryService = categoryService;
            this.walletCategoryService = walletCategoryService;
            this.walletBankService = walletBankService;
            this.resources = resources;

            Currency = "Kč";
            categoryOther = resources.GetString("Category_Other");
            brushes = new List<SolidColorBrush> { new SolidColorBrush(Colors.Black), new SolidColorBrush(Colors.DarkSlateGray) };
        }

        public async Task LoadDataAsync(Wallet wallet)
        {
            Wallet = wallet;
            await LoadCategoriesAsync();
            await LoadBanksAsync();

            foreach(Bank bank in Banks)
            {
                await LoadBankDataAsync(bank);
            }

            ComputeWalletInfo();
        }
        
        private async Task LoadCategoriesAsync()
        {
            var walletId = Wallet.Id;
            var filter = new WalletCategoryFilter() { WalletId = walletId };
            var modifier = new WalletCategoryModifier() { IncludeWholeCategory = true };
            var walletsCategories = await walletCategoryService.GetAsync(filter, modifier);
            var categories = new List<Category>();

            foreach (var walletCategory in walletsCategories)
            {
                if (walletCategory.Category != null)
                    categories.Add(walletCategory.Category);
            }

            Categories = new ObservableCollection<Category>(categories);
        }

        private async Task LoadBanksAsync()
        {
            var walletId = Wallet.Id;
            var filter = new WalletBankFilter() { WalletId = walletId };
            var modifier = new WalletBankModifier() { IncludeBank = true };
            var walletsBanks = await walletBankService.GetAsync(filter, modifier);
            var banks = new List<Bank>();

            foreach (var walletBank in walletsBanks)
            {
                if (walletBank.Bank != null)
                    banks.Add(walletBank.Bank);
            }

            Banks = new ObservableCollection<Bank>(banks);
        }

        public async Task LoadBankDataAsync(Bank bank)
        {
            if (bank != null)
            {
                bank.BankAccountInfo = await bankAccountInfoService.GetAsync(bank.Id);
                var filter = new Shared.Filters.TransactionFilter() { DateSince = DateTime.Now.AddMonths(-1).Date };
                bank.StoredTransactions = await transactionService.GetByBankIdAsync(bank.Id, filter);
            }

            ReloadTransactions();
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

            var filter = new BL.Filters.TransactionFilter();
            filter.AddMonths(1);

            try
            {
                
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

        private async Task SyncBank(Bank bank, BL.Filters.TransactionFilter filter)
        {
            var bankTransactions = await bank.GetTransactionsAsync(filter);

            if (bank.StoredTransactions != null && bank.StoredTransactions.Any())
            {
                var toStore = new List<Transaction>();
                foreach (var transaction in bankTransactions)
                {
                    if (!bank.StoredTransactions.Any(x => x.ExternalId == transaction.ExternalId))
                    {
                        bank.StoredTransactions.Add(transaction);
                        toStore.Add(transaction);
                    }
                }

                await transactionService.InsertOrIgnoreAsync(false, toStore.ToArray());
            }
            else
            {
                bank.StoredTransactions = bankTransactions;
                await transactionService.InsertOrIgnoreAsync(false, bankTransactions.ToArray());
            }
            
            ReloadTransactions();

            await bankAccountInfoService.InsertOrReplaceAsync(false, bank.BankAccountInfo);
        }

        private void SetSyncExecuteTimer()
        {
            if (syncExecuteTimer != null)
                syncExecuteTimer.Dispose();

            bool allCanSync = true;
            DateTime lastPossibleSyncTime = DateTime.Now.Date;

            foreach (Bank bank in Banks)
            {
                if (!(bank?.CanSyncExecute ?? true))
                {
                    allCanSync = false;

                    if (bank.NextPossibleSyncTime > lastPossibleSyncTime)
                    {
                        lastPossibleSyncTime = bank.NextPossibleSyncTime;
                    }
                }
            }

            if (!allCanSync)
            {
                var dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;
                syncExecuteTimer = new Timer(async (obj) => await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => NotifyPropertyChanged(nameof(SyncCommand))), null, (int)Math.Ceiling((lastPossibleSyncTime - DateTime.Now).TotalMilliseconds), Timeout.Infinite);
            }
        }

        private void ReloadTransactions()
        {
            NotifyPropertyChanged(nameof(TransactionCategories));

            if (Wallet == null)
            {
                Balances = null;
                Incomes = null;
                Expenses = null;
                return;
            }

            var mergedTrans = new List<Transaction>();
            var amount = 0.0;
            var closingBalance = 0.0;

            foreach (Bank bank in Banks)
            {
                amount = closingBalance = bank.BankAccountInfo.ClosingBalance;
                mergedTrans.AddRange(bank.StoredTransactions);
            }
            
            var trans = mergedTrans.GroupBy(x => x.Date).Select(x => Tuple.Create(x.Key, x.Sum(y => y.Amount))).OrderByDescending(x => x.Item1).Select(x =>
            {
                amount -= (double)x.Item2;
                return Tuple.Create(x.Item1, amount);
            }).ToList();

            if (trans.FirstOrDefault()?.Item1.Date == DateTime.Now.Date)
                trans.RemoveAt(0);

            trans.Add(Tuple.Create(DateTime.Now, closingBalance));
            Balances = trans;

            TransactionCategories = categoryService.GroupTransactionsForWallet(Categories, mergedTrans, categoryOther);
            Incomes = TransactionCategories.Where(x => x.Transactions.Any(y => y.Amount > 0)).Select(x => Tuple.Create(x.CategoryName, (double)x.Transactions.Where(y => y.Amount > 0).Sum(y => y.Amount))).ToList();
            Expenses = TransactionCategories.Where(x => x.Transactions.Any(y => y.Amount < 0)).Select(x => Tuple.Create(x.CategoryName, (double)x.Transactions.Where(y => y.Amount < 0).Sum(y => y.Amount))).ToList();
        }


        private void ComputeWalletInfo()
        {
            var closingBalance = 0.0;
            var openingBalance = 0.0;
            var openingDate = DateTime.Now;
            var lastSync = DateTime.Now;

            foreach (Bank bank in Banks)
            {
                closingBalance += bank.BankAccountInfo.ClosingBalance;
                openingBalance += bank.BankAccountInfo.OpeningBalance;

                var startDate = bank.BankAccountInfo.DateStartAsDate;
                if (openingDate == null || openingDate > startDate)
                {
                    openingDate = startDate;
                }

                var endDate = bank.BankAccountInfo.DateEndAsDate;
                if (lastSync == null || lastSync > endDate)
                {
                    lastSync = endDate;
                }
            }

            ClosingBalance = closingBalance;
            OpeningBalance = openingBalance;
            OpeningDate = openingDate;
            LastSync = lastSync;
        }
    }
}
