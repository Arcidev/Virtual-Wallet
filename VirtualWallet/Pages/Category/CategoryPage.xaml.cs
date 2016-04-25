using BL.Models;
using BL.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using VirtualWallet.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace VirtualWallet.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CategoryPage : Page
    {
        private CategoryPageViewModel viewModel;

        public CategoryPage()
        {
            this.InitializeComponent();
            viewModel = new CategoryPageViewModel(new CategoryService(), new WalletService(), new WalletCategoryService());
            this.DataContext = viewModel;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var category = (Category)e.Parameter;
            viewModel.Category = category;
            await viewModel.LoadDataAsync();
            base.OnNavigatedTo(e);
        }

        private void GridViewCategories_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void GridViewWallets_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}
