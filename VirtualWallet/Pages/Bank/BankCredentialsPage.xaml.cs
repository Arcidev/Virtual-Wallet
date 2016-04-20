using BL.Models;
using System;
using VirtualWallet.ViewModels;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace VirtualWallet.Pages
{
    public sealed partial class BankCredentialsPage : Page
    {
        private BankCredentialsPageViewModel viewModel;

        public BankCredentialsPage()
        {
            this.InitializeComponent();
            viewModel = new BankCredentialsPageViewModel();
            this.DataContext = viewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            viewModel.Bank = (Bank)e.Parameter;
        }

        private async void SubmitButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (!viewModel.IsValid)
            {
                var dialog = new MessageDialog("You have to set a token");
                dialog.Commands.Add(new UICommand("Close"));

                await dialog.ShowAsync();
                return;
            }

            viewModel.SetCredentials();
            Frame.Navigate(typeof(BankPage), viewModel.Bank);
        }

        private void TokenTextBox_KeyUp(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            var control = (Control)sender;
            if (e.Key == VirtualKey.Enter)
            {
                // Lose focus on the control to hide the virtual keyboard on mobile devices.
                // I know how it looks, but seriously there is no other way.
                control.IsEnabled = false;
                control.IsEnabled = true;
            }
        }
    }
}
