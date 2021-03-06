﻿using BL.Service;
using System;
using System.Linq;
using System.Threading.Tasks;
using VirtualWallet.Helpers;
using VirtualWallet.ViewModels;
using Windows.ApplicationModel.Resources;
using Windows.ApplicationModel.Resources.Core;
using Windows.Globalization;
using Windows.System.UserProfile;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace VirtualWallet.Pages
{
    public sealed partial class SettingsPage : Page
    {
        private readonly SettingsViewModel viewModel;
        private readonly ResourceLoader resources;

        public SettingsPage()
        {
            InitializeComponent();
            resources = ResourceLoader.GetForCurrentView();
            viewModel = new SettingsViewModel(new DatabaseService(), new BankService(), new CurrencyService(), resources);
            DataContext = viewModel;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            MenuHelper.SetHeader(resources.GetString("Settings_Header"));
            await viewModel.LoadDataAsync();
            base.OnNavigatedTo(e);
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
            dialog.Commands.Add(new UICommand(resources.GetString("Dialog_Yes"), yesHandler));
            dialog.Commands.Add(new UICommand(resources.GetString("Dialog_No")));

            await dialog.ShowAsync();
        }

        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ApplicationLanguages.PrimaryLanguageOverride == viewModel.SelectedLanguageCode)
                return;

            ApplicationLanguages.PrimaryLanguageOverride = viewModel.SelectedLanguageCode;
            // Hard way to force that language to be changed
            ResourceContext.GetForCurrentView().QualifierValues["language"] = string.IsNullOrEmpty(ApplicationLanguages.PrimaryLanguageOverride) ? GlobalizationPreferences.Languages.FirstOrDefault() ?? "" : ApplicationLanguages.PrimaryLanguageOverride;
            viewModel.ReloadTexts();
            MenuHelper.ReloadTexts(resources.GetString("Settings_Header"));
        }
    }
}
