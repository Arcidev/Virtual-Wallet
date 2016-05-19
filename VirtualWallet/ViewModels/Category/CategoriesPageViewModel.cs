using BL.Models;
using BL.Service;
using Shared.Filters;
using Shared.Modifiers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace VirtualWallet.ViewModels
{
    class CategoriesPageViewModel : ViewModelBase
    {
        private ICategoryService categoryService;
        private IWalletCategoryService walletCategoryService;

        private Wallet wallet;
        private ObservableCollection<Category> categories;

        private Category selectedCategory;

        public CategoriesPageViewModel(ICategoryService categoryService, IWalletCategoryService walletCategoryService)
        {
            this.categoryService = categoryService;
            this.walletCategoryService = walletCategoryService;
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
            }
        }

        public Category SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                if (selectedCategory == value)
                    return;

                selectedCategory = value;
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

        public async Task LoadDataAsync()
        {
            var categoryModifier = new CategoryModifier() { IncludeImage = true };
            var categories = await categoryService.GetAllAsync(categoryModifier);
            
            if (Wallet != null)
            {
                var walCatModifier = new WalletCategoryModifier() { IncludeCategory = true };
                var walCatfilter = new WalletCategoryFilter() { WalletId = Wallet.Id };
                var walletsCategories = await walletCategoryService.GetAsync(walCatfilter, walCatModifier);
                
                foreach (var walletCategory in walletsCategories)
                {
                    categories.Remove(walletCategory.Category);
                }
            }
            
            Categories = new ObservableCollection<Category>(categories);
        }

        public async Task SaveRelationAsync()
        {
            if (Wallet != null && SelectedCategory != null)
            {
                var walCat = new WalletCategory() { WalletId = Wallet.Id, CategoryId = SelectedCategory.Id};
                await walletCategoryService.InsertAsync(false, walCat);
            }
        }
    }
}
