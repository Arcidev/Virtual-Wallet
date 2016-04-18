using BL.Models;
using BL.Service;
using VirtualWallet.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace VirtualWallet
{
    public sealed partial class BanksPage : Page
    {
        private BanksPageViewModel viewModel;

        public BanksPage()
        {
            this.InitializeComponent();
            viewModel = new BanksPageViewModel(new BankService());
            this.DataContext = viewModel;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await viewModel.LoadData();
        }

        private void RelativePanel_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            var bank = (Bank)((RelativePanel)sender).DataContext;
            Frame.Navigate(bank.HasCredentials ? typeof(BankPage) : typeof(BankCredentialsPage), bank);
        }
    }
}
