using BL.Service;
using VirtualWallet.ViewModels;
using Windows.UI.Xaml.Controls;

namespace VirtualWallet
{
    public sealed partial class BankLoginPage : Page
    {
        private BankLoginPageViewModel viewModel;

        public BankLoginPage()
        {
            this.InitializeComponent();
            viewModel = new BankLoginPageViewModel(new BankService());
            this.DataContext = viewModel;
        }
    }
}
