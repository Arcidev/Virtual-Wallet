using BL.Models;
using BL.Service;
using System;
using VirtualWallet.ViewModels;
using Windows.Foundation.Metadata;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace VirtualWallet
{
    public sealed partial class BankPage : Page
    {
        private BankPageViewModel viewModel;

        public BankPage()
        {
            this.InitializeComponent();
            viewModel = new BankPageViewModel(new BankService());
            this.DataContext = viewModel;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            viewModel.Bank = (Bank)e.Parameter;

            //Mobile customization
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                var statusBar = StatusBar.GetForCurrentView();
                await statusBar.ShowAsync();
            }
        }
    }
}
