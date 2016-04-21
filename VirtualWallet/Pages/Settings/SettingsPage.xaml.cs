using BL.Service;
using System;
using VirtualWallet.ViewModels;
using Windows.ApplicationModel.Resources;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace VirtualWallet.Pages
{
    public sealed partial class SettingsPage : Page
    {
        private SettingsViewModel viewModel;
        private ResourceLoader resources;

        public SettingsPage()
        {
            this.InitializeComponent();
            viewModel = new SettingsViewModel(new DatabaseService(), new BankService());
            this.DataContext = viewModel;
            resources = new ResourceLoader();
        }

        private void RemoveCredentialsButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ShowDialog(string.Format(resources.GetString("Settings_RemoveDialog"), resources.GetString("Settings_RemoveDialog_Credentials")), viewModel.RemoveAllCredentialsCommand.Execute);
        }

        private void RemoveAllDataButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ShowDialog(string.Format(resources.GetString("Settings_RemoveDialog"), resources.GetString("Settings_RemoveDialog_Data")), viewModel.RemoveAllDataCommand.Execute);
        }

        private void CopyDbToRoamingButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ShowDialog(resources.GetString("Settings_CopyDbDialog"), viewModel.CopyDatabaseToRoamingFolderCommand.Execute);
        }

        private void RetrieveDbFromRoaming_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ShowDialog(resources.GetString("Settings_RetrieveDbDialog"), viewModel.RetrieveDatabaseFromRoamingFolderCommand.Execute);
        }

        private async void ShowDialog(string message, UICommandInvokedHandler yesHandler)
        {
            var dialog = new MessageDialog(message);
            dialog.Commands.Add(new UICommand(resources.GetString("Settings_Dialog_Yes"), yesHandler));
            dialog.Commands.Add(new UICommand(resources.GetString("Settings_Dialog_No")));

            await dialog.ShowAsync();
        }
    }
}
