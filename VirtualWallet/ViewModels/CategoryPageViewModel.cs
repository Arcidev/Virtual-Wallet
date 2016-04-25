using BL.Models;
using BL.Service;
using Shared.Filters;
using Shared.Modifiers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace VirtualWallet.ViewModels
{
    class CategoryPageViewModel : ViewModelBase
    {
        private ICategoryService categoryService;
        private IWalletService walletService;
        private IWalletCategoryService walletCategoryService;
        //private IRuleService ruleService;

        private Category category;
        private ObservableCollection<Wallet> wallets;
        private ObservableCollection<Rule> rules;

        public CategoryPageViewModel(ICategoryService categoryService, IWalletService walletService, IWalletCategoryService walletCategoryService)
        {
            this.categoryService = categoryService;
            this.walletService = walletService;
            this.walletCategoryService = walletCategoryService;
            //this.ruleService = ruleService;

            this.Category = new Category();
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

        public ObservableCollection<Wallet> Wallets
        {
            get { return wallets; }
            set
            {
                if (wallets == value)
                    return;

                wallets = value;
                category.Wallets = value;
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
                category.Rules = value;
                NotifyPropertyChanged();
            }
        }

        public async Task LoadDataAsync()
        {
            var categoryId = Category.Id;
            var filter = new WalletCategoryFilter() { CategoryId = categoryId };
            var modifier = new WalletCategoryModifier() { IncludeWallet = true };
            var walletsCategories = await walletCategoryService.GetAsync(filter, modifier);
            var wallets = new List<Wallet>();

            foreach (var walletCategory in walletsCategories)
            {
                if (walletCategory.Wallet != null)
                    wallets.Add(walletCategory.Wallet);
            }

            Wallets = new ObservableCollection<Wallet>(wallets);
        }
    }
}
