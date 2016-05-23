using BL.Models;
using BL.Service;
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
    public sealed partial class WalletsPage : Page
    {
        private WalletsPageViewModel viewModel;
        private ResourceLoader resources;

        public WalletsPage()
        {
            resources = ResourceLoader.GetForCurrentView();
            this.InitializeComponent();
            viewModel = new WalletsPageViewModel(new WalletService());
            this.DataContext = viewModel;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            this.setPageHeader();
            await viewModel.LoadDataAsync();
            base.OnNavigatedTo(e);
        }

        private void AddAppBarButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var pagePayload = new PagePayload() { Dto = new Wallet() { Name = resources.GetString("Wallet_WalletDefaultName") } };
            Frame.Navigate(typeof(WalletPage), pagePayload);
        }

        private void GridViewWallets_ItemClick(object sender, ItemClickEventArgs e)
        {
            var wallet = (Wallet)e.ClickedItem; ;
            var walletPageDto = new BL.Models.PagePayload() { Dto = wallet };
            Frame.Navigate(typeof(WalletPage), walletPageDto);
        }

        private void setPageHeader()
        {
            var rootFrame = Window.Current.Content as HamburgerFrame;
            var header = rootFrame.Header as HamburgerTitleBar;
            header.Title = resources.GetString("Wallets_PageTitle");
        }
    }
}
