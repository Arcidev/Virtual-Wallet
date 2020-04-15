using BL.Metadata;
using BL.Models;
using BL.Service;
using Shared.Enums;
using Shared.Filters;
using Shared.Formatters;
using Shared.Modifiers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
        private readonly IBankAccountInfoService bankAccountInfoService;
        private readonly ITransactionService transactionService;
        private readonly ICategoryService categoryService;
        private readonly IWalletCategoryService walletCategoryService;
        private readonly IWalletBankService walletBankService;
        private readonly ICurrencyService currencyService;
        private readonly ResourceLoader resources;
        private Wallet wallet;
        private ObservableCollection<Bank> banks;
        private ObservableCollection<Category> categories;
        private List<Tuple<string, double>> expenses;
        private List<Tuple<string, double>> incomes;
        private List<Tuple<DateTime, double>> balances;
        private List<TransactionCategoryList> transactionCategories;
        private List<Transaction> cashPayments;
        private CommandHandler syncCommand;
        private Timer syncExecuteTimer;
        private bool syncButtonForceDisabled;
        private readonly string categoryOther;
        private string linearAxisInfo;
        private double walletBalance;
        private double closingBalance;
        private double openingBalance;
        private DateTime openingDate;
        private DateTime lastSync;
        private string currency;

        public List<SolidColorBrush> Brushes { get; }

        public Action BeforeSync { get; set; }

        public Action AfterSync { get; set; }

        public bool HasTransactions => TransactionCategories?.Any() ?? false;

        public bool HasIncomes => Incomes?.Any() ?? false;

        public bool HasExpenses => Expenses?.Any() ?? false;

        public bool HasBalances => Balances?.Any() ?? false;

        public string LinearAxisInfo
        {
            get => linearAxisInfo;
            private set
            {
                if (linearAxisInfo == value)
                    return;

                linearAxisInfo = value;
                NotifyPropertyChanged();
            }
        }

        public CommandHandler SyncCommand
        {
            get => syncCommand;
            private set
            {
                if (syncCommand == value)
                    return;

                syncCommand = value;
                NotifyPropertyChanged();
            }
        }

        public List<TransactionCategoryList> TransactionCategories
        {
            get => transactionCategories;
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
            get => wallet;
            private set
            {
                if (wallet == value)
                    return;

                wallet = value;
                NotifyPropertyChanged();

                // BankAccountInfo = Bank?.BankAccountInfo ?? new BankAccountInfo();
            }
        }

        public double WalletBalance
        {
            get => walletBalance;
            private set
            {
                if (walletBalance == value)
                    return;

                walletBalance = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(WalletBalanceString));
            }
        }

        public string WalletBalanceString => CurrencyFormatter.Format(walletBalance, Currency);

        public double ClosingBalance
        {
            get => closingBalance;
            private set
            {
                if (closingBalance == value)
                    return;

                closingBalance = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(ClosingBalanceString));
            }
        }

        public string ClosingBalanceString => CurrencyFormatter.Format(closingBalance, Currency);

        public double OpeningBalance
        {
            get => openingBalance;
            private set
            {
                if (openingBalance == value)
                    return;

                openingBalance = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(OpeningBalanceString));
            }
        }

        public string OpeningBalanceString => CurrencyFormatter.Format(openingBalance, Currency);

        public DateTime OpeningDate
        {
            get => openingDate;
            private set
            {
                if (openingDate == value)
                    return;

                openingDate = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(OpeningDateString));
            }
        }

        public string OpeningDateString => DateTimeFormatter.ToShortDate(OpeningDate);

        public DateTime LastSync
        {
            get => lastSync;
            private set
            {
                if (lastSync == value)
                    return;

                lastSync = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(LastSyncString));
            }
        }

        public string LastSyncString => DateTimeFormatter.ToShortDate(LastSync);

        public string Currency
        {
            get => currency;
            private set
            {
                if (currency == value)
                    return;

                currency = value;
                NotifyPropertyChanged();
            }
        }

        public TimeRange TimeRange
        {
            get => Wallet == null ? TimeRange.Month : Wallet.TimeRange;
            set
            {
                if (Wallet == null || Wallet.TimeRange == value)
                    return;

                Wallet.TimeRange = value;
                NotifyPropertyChanged();
                LoadBanksInner();
            }
        }

        public ObservableCollection<Bank> Banks
        {
            get => banks;
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
            get => categories;
            private set
            {
                if (categories == value)
                    return;

                categories = value;
                NotifyPropertyChanged();
            }
        }

        public List<Tuple<string, double>> Expenses
        {
            get => expenses;
            private set
            {
                if (expenses == value)
                    return;

                expenses = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(HasExpenses));
            }
        }

        public List<Tuple<string, double>> Incomes
        {
            get => incomes;
            private set
            {
                if (incomes == value)
                    return;

                incomes = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(HasIncomes));
            }
        }

        public List<Tuple<DateTime, double>> Balances
        {
            get => balances;
            private set
            {
                if (balances == value)
                    return;

                balances = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(HasBalances));
            }
        }

        public WalletPageViewModel(IBankAccountInfoService bankAccountInfoService, ITransactionService transactionService, ICategoryService categoryService, IWalletCategoryService walletCategoryService, IWalletBankService walletBankService, ICurrencyService currencyService, ResourceLoader resources)
        {
            this.bankAccountInfoService = bankAccountInfoService;
            this.transactionService = transactionService;
            this.categoryService = categoryService;
            this.walletCategoryService = walletCategoryService;
            this.walletBankService = walletBankService;
            this.currencyService = currencyService;
            this.resources = resources;
            
            categoryOther = resources.GetString("Category_Other");
            Brushes = new List<SolidColorBrush> { new SolidColorBrush(Colors.Black), new SolidColorBrush(Colors.DarkSlateGray) };
        }

        public async Task LoadDataAsync(Wallet wallet)
        {
            Wallet = wallet;
            await LoadCurrencyAsync();
            await LoadCategoriesAsync();
            await LoadCashPaymentsAsync();
            await LoadBanksAsync();
            await LoadBanksDataAsync();
            await LoadCurrencyAsync();
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

        public async Task LoadBanksDataAsync()
        {
            foreach (Bank bank in Banks)
            {
                if (bank != null)
                {
                    bank.BankAccountInfo = await bankAccountInfoService.GetAsync(bank.Id);
                    var filter = new Shared.Filters.TransactionFilter() { DateSince = TimeRange.ToDateSince() };
                    bank.StoredTransactions = await transactionService.GetByBankIdAsync(bank.Id, filter);
                }
            }
            
            ReloadTransactions();
        }

        private async Task LoadCashPaymentsAsync()
        {
            var filter = new TransactionFilter() { IsCashPayment = true, DateSince = TimeRange.ToDateSince() };
            cashPayments = await transactionService.GetAsync(filter);
        }

        public async Task LoadCurrencyAsync()
        {
            if (Wallet?.CurrencyId != null)
            {
                var currency = await currencyService.GetAsync((int) Wallet.CurrencyId);
                Currency = currency.Code;
            }
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
            SyncCommand.NotifyCanExecuteChanged();

            var filter = new BL.Filters.TransactionFilter();
            filter.AddMonths(1);

            try
            {
                // Something should probably be here
            }
            catch (Exception)
            {
                var dialog = new MessageDialog(resources.GetString("Bank_ErrorDialog"));
                dialog.Commands.Add(new UICommand(resources.GetString("Dialog_Close")));

                await dialog.ShowAsync();
            }

            syncButtonForceDisabled = false;
            SyncCommand.NotifyCanExecuteChanged();
            SetSyncExecuteTimer();

            AfterSync?.Invoke();
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
                syncExecuteTimer = new Timer(async (obj) => await dispatcher.RunAsync(CoreDispatcherPriority.Normal, SyncCommand.NotifyCanExecuteChanged), null, (int)Math.Ceiling((lastPossibleSyncTime - DateTime.Now).TotalMilliseconds), Timeout.Infinite);
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

            if (cashPayments != null)
                mergedTrans.AddRange(cashPayments);

            var trans = mergedTrans.GroupBy(x => x.Date).Select(x => Tuple.Create(x.Key, x.Sum(y => y.Amount))).OrderByDescending(x => x.Item1).Select(x =>
            {
                amount -= (double)x.Item2;
                return Tuple.Create(x.Item1, amount);
            }).ToList();

            if (trans.FirstOrDefault()?.Item1.Date == DateTime.Now.Date)
                trans.RemoveAt(0);

            if (trans.Count() != 0)
            {
                trans.Insert(0, Tuple.Create(DateTime.Now, closingBalance));
                Balances = trans;

                TransactionCategories = categoryService.GroupTransactionsForWallet(Categories, mergedTrans, categoryOther);
                Incomes = TransactionCategories.Where(x => x.Transactions.Any(y => y.Amount > 0)).Select(x => Tuple.Create(x.CategoryName, (double)x.Transactions.Where(y => y.Amount > 0).Sum(y => y.Amount))).ToList();
                Expenses = TransactionCategories.Where(x => x.Transactions.Any(y => y.Amount < 0)).Select(x => Tuple.Create(x.CategoryName, (double)x.Transactions.Where(y => y.Amount < 0).Sum(y => y.Amount))).ToList();
            }
            else
            {
                Balances = null;
                Incomes = null;
                Expenses = null;
                TransactionCategories = null;
            }
            
            ComputeWalletInfo();
        }


        private void ComputeWalletInfo()
        {
            var cashPaymentBalance = 0.0;
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
            
            foreach(Transaction t in cashPayments)
            {
                cashPaymentBalance += decimal.ToDouble(t.Amount);
            }

            WalletBalance = closingBalance + cashPaymentBalance;
            ClosingBalance = closingBalance;
            OpeningBalance = openingBalance;
            OpeningDate = openingDate;
            LastSync = lastSync;
        }

        private async void LoadBanksInner()
        {
            await LoadBanksDataAsync();
        }
    }
}
