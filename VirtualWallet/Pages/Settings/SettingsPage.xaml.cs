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
            ShowRemoveDataDialog(resources.GetString("Settings_RemoveDialog_Credentials"), viewModel.RemoveAllCredentialsCommand.Execute);
        }

        private void RemoveAllDataButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ShowRemoveDataDialog(resources.GetString("Settings_RemoveDialog_Data"), viewModel.RemoveAllDataCommand.Execute);
        }

        private async void ShowRemoveDataDialog(string dataToRemove, UICommandInvokedHandler yesHandler)
        {
            var dialog = new MessageDialog(string.Format(resources.GetString("Settings_RemoveDialog"), dataToRemove));
            dialog.Commands.Add(new UICommand(resources.GetString("Settings_RemoveDialog_Yes"), yesHandler));
            dialog.Commands.Add(new UICommand(resources.GetString("Settings_RemoveDialog_No")));

            await dialog.ShowAsync();
        }
    }
}
