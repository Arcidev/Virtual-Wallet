using BL.Service;
using System;
using VirtualWallet.ViewModels;
using Windows.UI.Popups;
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

        private void RemoveCredentialsButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ShowRemoveDataDialog("all credentials", viewModel.RemoveAllCredentialsCommand.Execute);
        }

        private void RemoveAllDataButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ShowRemoveDataDialog("all data and credentials", viewModel.RemoveAllDataCommand.Execute);
        }

        private async void ShowRemoveDataDialog(string dataToRemove, UICommandInvokedHandler yesHandler)
        {
            var dialog = new MessageDialog($"By performing this action you will erase {dataToRemove} stored by this application. Are you sure you want to continue?");
            dialog.Commands.Add(new UICommand("Yes", yesHandler));
            dialog.Commands.Add(new UICommand("No"));

            await dialog.ShowAsync();
        }
    }
}
