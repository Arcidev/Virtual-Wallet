using BL.Models;
using BL.Service;
using BL.Service.Menu;
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
            MenuUnil.setHeader("Wallets_PageTitle");
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
    }
}
