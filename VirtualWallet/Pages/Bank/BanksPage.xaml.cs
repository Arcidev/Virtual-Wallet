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
    public sealed partial class BanksPage : Page
    {
        private BanksPageViewModel viewModel;
        private PagePayload pagePayload;
        private ResourceLoader resources;

        public BanksPage()
        {
            resources = ResourceLoader.GetForCurrentView();
            this.InitializeComponent();
            viewModel = new BanksPageViewModel(new BankService(), new WalletBankService());
            this.DataContext = viewModel;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            this.setPageHeader();

            pagePayload = (PagePayload)e.Parameter;
            viewModel.Wallet = (Wallet)pagePayload.Dto;
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

        private void setPageHeader()
        {
            var rootFrame = Window.Current.Content as HamburgerFrame;
            var header = rootFrame.Header as HamburgerTitleBar;
            header.Title = resources.GetString("Banks_PageTitle");
        }
    }
}
