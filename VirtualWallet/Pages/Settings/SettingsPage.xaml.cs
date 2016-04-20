using BL.Service;
using VirtualWallet.ViewModels;
using Windows.UI.Xaml.Controls;

namespace VirtualWallet.Pages
{
    public sealed partial class SettingsPage : Page
    {
        private SettingsViewModel viewModel;

        public SettingsPage()
        {
            this.InitializeComponent();
            viewModel = new SettingsViewModel(new DatabaseService(), new BankService());
        }
    }
}
