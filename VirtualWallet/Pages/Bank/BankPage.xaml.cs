using BL.Models;
using BL.Service;
using VirtualWallet.ViewModels;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace VirtualWallet.Pages
{
    public sealed partial class BankPage : Page
    {
        private BankPageViewModel viewModel;

        public BankPage()
        {
            this.InitializeComponent();
            viewModel = new BankPageViewModel(new BankAccountInfoService(), new ResourceLoader());
            this.DataContext = viewModel;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await viewModel.LoadDataAsync((Bank)e.Parameter);
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            viewModel.Dispose();
            base.OnNavigatingFrom(e);
        }

        private void EditAppBarButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(BankCredentialsPage), viewModel.Bank);
        }

        private void SettingsAppBarButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SettingsPage));
        }
    }
}
