using BL.Service;
using VirtualWallet.ViewModels;
using Windows.UI.Xaml.Controls;

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
    }
}
