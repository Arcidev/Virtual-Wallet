using BL.Models;
using BL.Service;
using VirtualWallet.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace VirtualWallet.Pages
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
            Frame.Navigate(bank.HasCredentials ? typeof(BankPage) : typeof(BankCredentialsPage), bank);
        }

        private void GridViewCategories_ItemClick(object sender, ItemClickEventArgs e)
        {
            Category category = (Category)e.ClickedItem; ;
            Frame.Navigate(typeof(CategoryPage), category);
        }

        private void SettingsAppBarButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SettingsPage));
        }
    }
}
