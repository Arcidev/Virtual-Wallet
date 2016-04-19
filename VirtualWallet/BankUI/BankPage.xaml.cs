using BL.Models;
using BL.Service;
using VirtualWallet.ViewModels;
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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            viewModel.Bank = (Bank)e.Parameter;
        }
    }
}
