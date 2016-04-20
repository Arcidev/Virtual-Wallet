using BL.Models;
using BL.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualWallet.CategoryUI;
using VirtualWallet.RuleUI;
using VirtualWallet.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace VirtualWallet
{
    public sealed partial class MenuPage : Page
    {
        private MenuPageViewModel viewModel;

        public MenuPage()
        {
            this.InitializeComponent();
            viewModel = new MenuPageViewModel(new WalletService(), new BankService(), new CategoryService());
            this.DataContext = viewModel;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await viewModel.LoadData();
            base.OnNavigatedTo(e);
        }

        private void GridViewWallets_ItemClick(object sender, ItemClickEventArgs e)
        {
            //WalletViewModel s = e.ClickedItem as WalletViewModel;
            //this.Frame.Navigate(typeof(WalletDetailPage), s);
        }

        private void GridViewBanks_ItemClick(object sender, ItemClickEventArgs e)
        {
            Bank bank = (Bank)e.ClickedItem;
            this.Frame.Navigate(bank.HasCredentials ? typeof(BankPage) : typeof(BankCredentialsPage), bank);
        }

        private void GridViewCategories_ItemClick(object sender, ItemClickEventArgs e)
        {
            CategoryPageViewModel s = e.ClickedItem as CategoryPageViewModel;
            this.Frame.Navigate(typeof(CategoryPage), s);
        }

        private void RulesButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(RulesPage));
        }
    }
}
