using BL.Models;
using BL.Service;
using Shared.Enums;
using Shared.Filters;
using Shared.Modifiers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using VirtualWallet.Controls;

namespace VirtualWallet.ViewModels
{
    class WalletEditPageViewModel : ViewModelBase
    {
        private ICategoryService categoryService;
        private IWalletService walletService;
        private IWalletCategoryService walletCategoryService;
        private IWalletBankService walletBankService;
        private ICurrencyService currencyService;

        private Wallet wallet;
        private Currency selectedCurrency;
        private Currency defaultCurrency;
        private ObservableCollection<Category> categories;
        private ObservableCollection<Bank> banks;
        private ObservableCollection<Currency> currencies;
        private Boolean modified;
        private Boolean persisted;

        public ICommand DeleteWalletCommand { get; private set; }

        public WalletEditPageViewModel(ICategoryService categoryService, IWalletService walletService, IWalletCategoryService walletCategoryService, IWalletBankService walletBankService, ICurrencyService currencyService)
        {
            this.categoryService = categoryService;
            this.walletService = walletService;
            this.walletCategoryService = walletCategoryService;
            this.walletBankService = walletBankService;
            this.currencyService = currencyService;
            this.modified = false;

            DeleteWalletCommand = new CommandHandler(DeleteWallet);

            this.Wallet = new Wallet();       
        }

        public Wallet Wallet
        {
            get { return wallet; }
            set
            {
                if (wallet == value)
                    return;

                wallet = value;
                
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(Name));
                NotifyPropertyChanged(nameof(Image));
                NotifyPropertyChanged(nameof(TimeRange));
                NotifyPropertyChanged(nameof(SelectedCurrency));
            }
        }

        public Boolean Modified
        {
            get
            {
                return modified;
            }
            set
            {
                if (modified == value)
                    return;

                modified = value;
                NotifyPropertyChanged();
            }
        }

        public Boolean Persisted
        {
            get
            {
                return persisted;
            }
            set
            {
                if (persisted == value)
                    return;

                persisted = value;
                NotifyPropertyChanged();
            }
        }

        public String Name
        {
            get
            { 
                return Wallet == null ? string.Empty : Wallet.Name;
            }
            set
            {
                if (Wallet == null || Wallet.Name == value)
                    return;

                Wallet.Name = value;
                Modified = true;
                NotifyPropertyChanged();
            }
        }

        public Image Image
        {
            get
            {
                return Wallet?.Image;
            }
            set
            {
                if (Wallet == null || value == null || value.Equals(Wallet.Image))
                    return;

                Wallet.Image = value;
                Modified = true;
                NotifyPropertyChanged();
            }
        }

        public TimeRange TimeRange
        {
            get
            {
                return Wallet == null ? TimeRange.Month : Wallet.TimeRange;
            }
            set
            {
                if (Wallet == null || Wallet.TimeRange == value)
                    return;

                Wallet.TimeRange = value;
                Modified = true;
                NotifyPropertyChanged();
            }
        }

        public Currency SelectedCurrency
        {
            get { return selectedCurrency; }
            set
            {
                if (selectedCurrency == value || Wallet == null)
                    return;

                selectedCurrency = value;
                Wallet.CurrencyId = value.Id;
                Modified = true;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<Currency> Currencies
        {
            get { return currencies; }
            set
            {
                if (currencies == value)
                    return;

                currencies = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<Category> Categories
        {
            get { return categories; }
            set
            {
                if (categories == value)
                    return;

                categories = value;
                wallet.Categories = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<Bank> Banks
        {
            get { return banks; }
            set
            {
                if (banks == value)
                    return;

                banks = value;
                NotifyPropertyChanged();
            }
        }

        public async Task LoadDataAsync()
        {
            await LoadCurrencies();
            await LoadWalletAsync();
            await LoadCategoriesAsync();
            await LoadBanksAsync();
        }

        private async Task LoadWalletAsync()
        {
            var modifier = new WalletModifier() { IncludeImage = true };
            var wallet = await walletService.GetAsync(Wallet.Id, modifier);

            if (wallet == null)
            {
                Persisted = false;
                Wallet.TimeRange = TimeRange.Month;
                SelectedCurrency = defaultCurrency;
                Modified = true; //User can save new category.
            }
            else
            {
                foreach(Currency c in Currencies)
                {
                    if (c.Id == wallet.CurrencyId)
                    {
                        SelectedCurrency = c;
                    }
                }

                Wallet = wallet;
                Persisted = true;
                Modified = false;
            }
        }

        private async Task LoadCategoriesAsync()
        {
            var walletId = Wallet.Id;
            var filter = new WalletCategoryFilter() { WalletId = walletId };
            var modifier = new WalletCategoryModifier() { IncludeCategory = true };
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

        private async Task LoadCurrencies()
        {
            var currencies = await currencyService.GetAllAsync();
            Currencies = new ObservableCollection<Currency>(currencies);

            foreach (Currency c in currencies)
            {
                if (c.IsDefaultCurrency)
                {
                    defaultCurrency = c;
                    break;
                }
            }
        }

        public async Task SaveWalletAsync()
        {
            if (Persisted)
            {
                await walletService.UpdateAsync(Wallet);
            }
            else
            {
                await walletService.InsertAsync(true, Wallet);
            }
            Modified = false;
        }

        public async Task DiscardChangesAsync()
        {
            await LoadWalletAsync();
        }

        public async void DeleteWallet()
        {
            await DeleteWalletCategoryAsync(Wallet.Id);
            await walletService.DeleteAsync(Wallet.Id);
            Wallet = null;
        }

        public async Task DeleteWalletAsync()
        {
            await DeleteWalletCategoryAsync(Wallet.Id);
            await walletService.DeleteAsync(Wallet.Id);
            Wallet = null;
        }

        public async Task DeleteWalletCategoryAsync(int walletId)
        {
            var filter = new WalletCategoryFilter() { WalletId = walletId };
            var walCats = await walletCategoryService.GetAsync(filter);

            foreach (WalletCategory walCat in walCats)
            {
                await walletCategoryService.DeleteAsync(walCat.Id);
            }
        }

        public async Task DetachCategoryAsync(Category category)
        {
            var filter = new WalletCategoryFilter() { WalletId = Wallet.Id, CategoryId = category.Id };
            var walCats = await walletCategoryService.GetAsync(filter);

            foreach (WalletCategory walCat in walCats)
            {
                await walletCategoryService.DeleteAsync(walCat.Id);
            }

            Categories.Remove(Categories.First(x => x.Id == category.Id));
        }

        public async Task DetachBankAsync(Bank bank)
        {
            var filter = new WalletBankFilter() { WalletId = Wallet.Id, BankId = bank.Id };
            var walCats = await walletBankService.GetAsync(filter);

            foreach (WalletBank walCat in walCats)
            {
                await walletBankService.DeleteAsync(walCat.Id);
            }

            Banks.Remove(Banks.First(x => x.Id == bank.Id));
        }
    }
}
