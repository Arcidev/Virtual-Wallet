using BL.Models;
using BL.Service;
using Shared.Modifiers;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace VirtualWallet.ViewModels
{
    class CategoryPageViewModel : ViewModelBase
    {
        private ICategoryService categoryService;
        private IWalletService walletService;
        //private IRuleService ruleService;

        private Category category;
        private ObservableCollection<Wallet> wallets;
        private ObservableCollection<Rule> rules;

        public CategoryPageViewModel(ICategoryService categoryService, IWalletService walletService)
        {
            this.categoryService = categoryService;
            this.walletService = walletService;
            //this.ruleService = ruleService;
        }

        public Category Category
        {
            get { return category; }
            set
            {
                if (category == value)
                    return;

                category = value;
                NotifyPropertyChanged();
            }
        }

        public String Name
        {
            get { return category.Name; }
            set
            {
                if (category.Name == value)
                    return;

                category.Name = value;
                NotifyPropertyChanged();
            }
        }

        public Image Image
        {
            get { return category.Image; }
            set
            {
                if (category.Image == value)
                    return;

                category.Image = value;
                NotifyPropertyChanged();
            }
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

        public ObservableCollection<Rule> Rules
        {
            get { return rules; }
            set
            {
                if (rules == value)
                    return;

                rules = value;
                NotifyPropertyChanged();
            }
        }

        public async Task LoadData()
        {
            var walletModifier = new WalletModifier() { IncludeImage = true };
            Wallets = new ObservableCollection<Wallet>(await walletService.GetAll(walletModifier));
        }


    }
}
