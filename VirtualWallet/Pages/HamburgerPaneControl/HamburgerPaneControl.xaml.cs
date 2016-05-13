using BL.Service;
using VirtualWallet.ViewModels;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace VirtualWallet.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HamburgerPaneControl : UserControl
    {
        private HamburgerPaneViewModel viewModel;

        public HamburgerPaneControl()
        {
            this.InitializeComponent();
            viewModel = new HamburgerPaneViewModel(new BankService());
            this.DataContext = viewModel;
        }

        private async void UserControl_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await viewModel.LoadDataAsync();
        }
    }
}
