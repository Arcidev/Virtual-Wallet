using BL.Models;
using BL.Service;
using System;
using VirtualWallet.ViewModels;
using Windows.ApplicationModel.Resources;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Navigation;

namespace VirtualWallet.Pages
{
    public sealed partial class MenuPage : Page
    {
        private MenuPageViewModel viewModel;
        private ResourceLoader resources;

        public MenuPage()
        {
            this.InitializeComponent();
            resources = new ResourceLoader();
            viewModel = new MenuPageViewModel(new WalletService(), new BankService(), new CategoryService());
            this.DataContext = viewModel;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await viewModel.LoadDataAsync();
            base.OnNavigatedTo(e);
        }

        private void GridViewWallets_ItemClick(object sender, ItemClickEventArgs e)
        {
            //WalletViewModel s = e.ClickedItem as WalletViewModel;
            //this.Frame.Navigate(typeof(WalletDetailPage), s);
        }

        private void GridViewBanks_ItemClick(object sender, ItemClickEventArgs e)
        {
            Bank bank = (Bank)e.ClickedItem;
            Frame.Navigate(bank.HasCredentials ? typeof(BankPage) : typeof(BankCredentialsPage), bank);
        }

        private void GridViewCategories_ItemClick(object sender, ItemClickEventArgs e)
        {
            var category = (Category)e.ClickedItem; ;
            var categoryPageDto = new BL.Models.PagePayload() { Dto = category };
            Frame.Navigate(typeof(CategoryPage), categoryPageDto);
        }

        private void SettingsAppBarButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SettingsPage));
        }

        private void BanksFlyoutEditButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(sender is FrameworkElement))
                return;

            var bankContext = ((FrameworkElement)sender).DataContext as Bank;
            if (bankContext == null)
                return;

            Frame.Navigate(typeof(BankCredentialsPage), bankContext);
        }

        private async void BanksFlyoutRemoveCredentialsButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(sender is FrameworkElement))
                return;

            var bankContext = ((FrameworkElement)sender).DataContext as Bank;
            if (bankContext == null)
                return;

            var dialog = new MessageDialog(resources.GetString("Menu_BankRemoveDialog"));
            int commandId = 1;
            dialog.Commands.Add(new UICommand(resources.GetString("Dialog_Yes"), null, commandId));
            dialog.Commands.Add(new UICommand(resources.GetString("Dialog_No")));

            var command = await dialog.ShowAsync();
            if (command.Id as int? != commandId)
                return;

            bankContext.RemoveCredentials();
        }

        private void GridViewBanks_RightTapped(object sender, Windows.UI.Xaml.Input.RightTappedRoutedEventArgs e)
        {
            if (!(sender is FrameworkElement) || !(e.OriginalSource is FrameworkElement))
                return;

            FlyoutBase flyoutBase = FlyoutBase.GetAttachedFlyout((FrameworkElement)sender);
            flyoutBase.ShowAt((FrameworkElement)e.OriginalSource);
            e.Handled = true;
        }
    }
}
