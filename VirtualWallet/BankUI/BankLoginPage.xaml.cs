using BL.Models;
using VirtualWallet.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace VirtualWallet
{
    public sealed partial class BankLoginPage : Page
    {
        private BankLoginPageViewModel viewModel;

        public BankLoginPage()
        {
            this.InitializeComponent();
            viewModel = new BankLoginPageViewModel();
            this.DataContext = viewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            viewModel.Bank = (Bank)e.Parameter;
        }
    }
}
