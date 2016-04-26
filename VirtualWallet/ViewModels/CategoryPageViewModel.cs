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
        private ICategoryRuleService categoryRuleService;

        private Category category;
        private ObservableCollection<Wallet> wallets;
        private ObservableCollection<Rule> rules;

        public CategoryPageViewModel(ICategoryService categoryService, IWalletService walletService, IWalletCategoryService walletCategoryService, ICategoryRuleService categoryRuleService)
        {
            this.categoryService = categoryService;
            this.walletService = walletService;
            this.walletCategoryService = walletCategoryService;
            this.categoryRuleService = categoryRuleService;
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
            await LoadWalletsAsync();
            await LoadRulesAsync();
        }

        private async Task LoadWalletsAsync()
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

        private async Task LoadRulesAsync()
        {
            var categoryId = Category.Id;
            var filter = new CategoryRuleFilter() { CategoryId = categoryId };
            var modifier = new CategoryRuleModifier() { IncludeRule = true };
            var categoriesRules = await categoryRuleService.GetAsync(filter, modifier);
            var rules = new List<Rule>();

            foreach (var categoryRule in categoriesRules)
            {
                if (categoryRule.Rule != null)
                    rules.Add(categoryRule.Rule);
            }

            Rules = new ObservableCollection<Rule>(rules);
        }
    }
}
