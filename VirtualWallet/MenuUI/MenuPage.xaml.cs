using BL.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        }

        private void GridViewCategories_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}
