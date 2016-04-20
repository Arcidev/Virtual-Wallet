using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Service;
using System.Collections.ObjectModel;
using BL.Models;
using Shared.Modifiers;

namespace VirtualWallet.ViewModels
{
    class MenuPageViewModel : ViewModelBase
    {
        private IBankService bankService;
        private ICategoryService categoryService;
        private IWalletService walletService;

        private ObservableCollection<Wallet> wallets;
        private ObservableCollection<Bank> banks;
        private ObservableCollection<Category> categories;

        public MenuPageViewModel(IWalletService walletService, IBankService bankService, ICategoryService categoryService)
        {
            this.walletService = walletService;
            this.bankService = bankService;
            this.categoryService = categoryService;
        }

        public async Task LoadData()
        {
            var walletModifier = new WalletModifier() { IncludeImage = true };
            Wallets = new ObservableCollection<Wallet>(await walletService.GetAll(walletModifier));

            var bankModifier = new BankModifier() { IncludeImage = true };
            Banks = new ObservableCollection<Bank>(await bankService.GetAll(bankModifier));

            var categoryModifier = new CategoryModifier() { IncludeImage = true };
            Categories = new ObservableCollection<Category>(await categoryService.GetAll(categoryModifier));
        }

        public ObservableCollection<Wallet> Wallets
        {
            get { return wallets; }
            set
            {
                if (wallets == value)
                    return;

                wallets = value;
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

        public ObservableCollection<Category> Categories
        {
            get { return categories; }
            set
            {
                if (categories == value)
                    return;

                categories = value;
                NotifyPropertyChanged();
            }
        }
    }
}
