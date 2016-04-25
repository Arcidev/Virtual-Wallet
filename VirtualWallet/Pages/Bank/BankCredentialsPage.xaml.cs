﻿using BL.Models;
using System;
using VirtualWallet.ViewModels;
using Windows.ApplicationModel.Resources;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace VirtualWallet.Pages
{
    public sealed partial class BankCredentialsPage : Page
    {
        private BankCredentialsPageViewModel viewModel;
        private ResourceLoader resources;

        public BankCredentialsPage()
        {
            this.InitializeComponent();
            viewModel = new BankCredentialsPageViewModel();
            resources = new ResourceLoader();
            this.DataContext = viewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            viewModel.Bank = (Bank)e.Parameter;
            base.OnNavigatedTo(e);
        }

        private async void SubmitButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (!viewModel.IsValid)
            {
                var dialog = new MessageDialog(resources.GetString("BankCredentials_Dialog"));
                dialog.Commands.Add(new UICommand(resources.GetString("Dialog_Close")));

                await dialog.ShowAsync();
                return;
            }

            viewModel.SetCredentials();
            if (Frame.Navigate(typeof(BankPage), viewModel.Bank))
                this.Frame.BackStack.RemoveAt(this.Frame.BackStack.Count - 1);
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
