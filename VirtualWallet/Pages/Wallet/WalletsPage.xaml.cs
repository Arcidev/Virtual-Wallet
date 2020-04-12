using BL.Models;
using BL.Service;
using VirtualWallet.Helpers;
using VirtualWallet.ViewModels;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace VirtualWallet.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WalletsPage : Page
    {
        private readonly WalletsPageViewModel viewModel;
        private readonly ResourceLoader resources;

        public WalletsPage()
        {
            resources = ResourceLoader.GetForCurrentView();
            InitializeComponent();
            viewModel = new WalletsPageViewModel(new WalletService());
            DataContext = viewModel;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            MenuHelper.SetHeader(resources.GetString("Wallets_PageTitle"));
            await viewModel.LoadDataAsync();
            base.OnNavigatedTo(e);
        }

        private void AddAppBarButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var pagePayload = new PagePayload() { Dto = new Wallet() { Name = resources.GetString("Wallet_WalletDefaultName") } };
            Frame.Navigate(typeof(WalletEditPage), pagePayload);
        }

        private void GridViewWallets_ItemClick(object sender, ItemClickEventArgs e)
        {
            var wallet = (Wallet)e.ClickedItem;
            Frame.Navigate(typeof(WalletPage), wallet);
        }
    }
}
