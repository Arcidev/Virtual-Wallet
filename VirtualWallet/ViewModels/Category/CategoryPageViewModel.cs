using BL.Models;
using BL.Service;
using Shared.Filters;
using Shared.Modifiers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualWallet.ViewModels
{
    class CategoryPageViewModel : ViewModelBase
    {
        private ICategoryService categoryService;
        private IWalletService walletService;
        private IWalletCategoryService walletCategoryService;
        private IRuleService ruleService;

        private Category category;
        private ObservableCollection<Wallet> wallets;
        private ObservableCollection<Rule> rules;
        private Boolean modified;

        public CategoryPageViewModel(ICategoryService categoryService, IWalletService walletService, IWalletCategoryService walletCategoryService, IRuleService ruleService)
        {
            this.categoryService = categoryService;
            this.walletService = walletService;
            this.walletCategoryService = walletCategoryService;
            this.ruleService = ruleService;
            this.modified = false;

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

        public String Name
        {
            get
            { 
                return Category == null ? string.Empty : Category.Name;
            }
            set
            {
                if (Category == null || Category.Name == value)
                    return;

                Category.Name = value;
                Modified = true;
                NotifyPropertyChanged();
            }
        }

        public Image Image
        {
            get
            {
                return Category?.Image;
            }
            set
            {
                if (Category == null || Category.Image == value)
                    return;

                Category.Image = value;
                Modified = true;
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
            var filter = new RuleFilter() { CategoryId = categoryId };
            Rules = new ObservableCollection<Rule>(await ruleService.GetAsync(filter));
        }

        public async Task SaveCategoryAsync()
        {
            await categoryService.InsertOrReplaceAsync(false, Category);
            Modified = false;
        }

        public async Task DiscardChangesAsync()
        {
            var catModifier = new CategoryModifier() { IncludeImage = true };
            var originalCategory = await categoryService.GetAsync(Category.Id, catModifier);
            await this.LoadDataAsync();

            Name = originalCategory.Name;
            Image = originalCategory.Image;

            Modified = false;
        }

        public async Task DetachRuleAsync(Rule rule)
        {
            Rules.Remove(Rules.First(x => x.Id == rule.Id));
            await ruleService.DeleteAsync(rule.Id);
        }
    }
}
