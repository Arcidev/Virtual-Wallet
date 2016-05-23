using BL.Models;
using BL.Service;
using BL.Service.Menu;
using Cimbalino.Toolkit.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using VirtualWallet.ViewModels;
using Windows.ApplicationModel.Resources;
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
    public sealed partial class CategoriesPage : Page
    {
        private CategoriesPageViewModel viewModel;
        private PagePayload pagePayload;
        private ResourceLoader resources;

        public CategoriesPage()
        {
            resources = ResourceLoader.GetForCurrentView();
            this.InitializeComponent();
            viewModel = new CategoriesPageViewModel(new CategoryService(), new WalletCategoryService());
            this.DataContext = viewModel;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            MenuUnil.setHeader("Categories_PageTitle");

            pagePayload = (PagePayload)e.Parameter;

            if (pagePayload != null)
            {
                CommandBar_Add.Visibility = Visibility.Collapsed;
                viewModel.Wallet = (Wallet)pagePayload?.Dto;
            }
            else
            {
                GridViewCategories.IsItemClickEnabled = true;
                CommandBar_Accept.Visibility = Visibility.Collapsed;
                CommandBar_Cancel.Visibility = Visibility.Collapsed;
            }
            
            await viewModel.LoadDataAsync();

            base.OnNavigatedTo(e);
        }

        private async void AcceptAppBarButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await viewModel.SaveRelationAsync();

            if (Frame.CanGoBack)
                Frame.GoBack();
        }

        private void CancelAppBarButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
                Frame.GoBack();
        }

        private void AddAppBarButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var pagePayload = new PagePayload() { Dto = new Category() { Name = resources.GetString("Category_CategoryDefaultName") } };
            Frame.Navigate(typeof(CategoryPage), pagePayload);
        }

        private void GridViewCategories_ItemClick(object sender, ItemClickEventArgs e)
        {
            var category = (Category)e.ClickedItem; ;
            var categoryPageDto = new BL.Models.PagePayload() { Dto = category };
            Frame.Navigate(typeof(CategoryPage), categoryPageDto);
        }
    }
}
