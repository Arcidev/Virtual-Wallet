using BL.Service;
using System;
using System.Threading.Tasks;
using VirtualWallet.ViewModels;
using Windows.ApplicationModel.Resources;
using Windows.Globalization;
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
            resources = new ResourceLoader();
            viewModel = new SettingsViewModel(new DatabaseService(), new BankService(), resources);
            this.DataContext = viewModel;
        }

        private async void RemoveCredentialsButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await ShowDialog(string.Format(resources.GetString("Settings_RemoveDialog"), resources.GetString("Settings_RemoveDialog_Credentials")), viewModel.RemoveAllCredentialsCommand.Execute);
        }

        private async void RemoveAllDataButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await ShowDialog(string.Format(resources.GetString("Settings_RemoveDialog"), resources.GetString("Settings_RemoveDialog_Data")), viewModel.RemoveAllDataCommand.Execute);
        }

        private async void CopyDbToRoamingButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await ShowDialog(resources.GetString("Settings_CopyDbDialog"), viewModel.CopyDatabaseToRoamingFolderCommand.Execute);
        }

        private async void RetrieveDbFromRoaming_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await ShowDialog(resources.GetString("Settings_RetrieveDbDialog"), viewModel.RetrieveDatabaseFromRoamingFolderCommand.Execute);
        }

        private async Task ShowDialog(string message, UICommandInvokedHandler yesHandler)
        {
            var dialog = new MessageDialog(message);
            dialog.Commands.Add(new UICommand(resources.GetString("Settings_Dialog_Yes"), yesHandler));
            dialog.Commands.Add(new UICommand(resources.GetString("Settings_Dialog_No")));

            await dialog.ShowAsync();
        }

        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ApplicationLanguages.PrimaryLanguageOverride == viewModel.SelectedLanguageCode)
                return;

            ApplicationLanguages.PrimaryLanguageOverride = viewModel.SelectedLanguageCode;
            if (this.Frame.Navigate(GetType()))
                this.Frame.BackStack.RemoveAt(this.Frame.BackStack.Count - 1);
        }
    }
}
