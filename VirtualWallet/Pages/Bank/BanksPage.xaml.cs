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
    public sealed partial class BanksPage : Page
    {
        private readonly BanksPageViewModel viewModel;
        private PagePayload pagePayload;
        private readonly ResourceLoader resources;

        public BanksPage()
        {
            resources = ResourceLoader.GetForCurrentView();
            InitializeComponent();
            viewModel = new BanksPageViewModel(new BankService(), new WalletBankService());
            DataContext = viewModel;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            MenuHelper.SetHeader(resources.GetString("Banks_PageTitle"));

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
    }
}
