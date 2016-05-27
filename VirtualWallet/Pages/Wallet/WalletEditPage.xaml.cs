using BL.Models;
using BL.Service;
using System;
using System.Threading.Tasks;
using VirtualWallet.Helpers;
using VirtualWallet.ViewModels;
using Windows.ApplicationModel.Resources;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace VirtualWallet.Pages
{
    public sealed partial class WalletEditPage : Page
    {
        private WalletEditPageViewModel viewModel;
        private PagePayload pagePayload;
        private ResourceLoader resources;

        public WalletEditPage()
        {
            this.InitializeComponent();
            resources = ResourceLoader.GetForCurrentView();
            viewModel = new WalletEditPageViewModel(new CategoryService(), new WalletService(), new WalletCategoryService(), new WalletBankService());
            this.DataContext = viewModel;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            MenuHelper.SetHeader(resources.GetString("Wallet_PageTitle"));

            pagePayload = (PagePayload)e.Parameter;
            viewModel.Wallet = (Wallet)pagePayload.Dto;
            await viewModel.LoadDataAsync();

            if (pagePayload.NewImage != null)
            {
                viewModel.Image = pagePayload.NewImage;
            }

            base.OnNavigatedTo(e);
        }

        protected override async void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            if (viewModel.Modified && viewModel.Persisted)
            {
                await SaveWallet();
            }
        }

        private async Task ShowDialog(string message, UICommandInvokedHandler yesHandler)
        {
            var dialog = new MessageDialog(message);
            dialog.Commands.Add(new UICommand(resources.GetString("Dialog_Yes"), yesHandler));
            dialog.Commands.Add(new UICommand(resources.GetString("Dialog_No")));

            await dialog.ShowAsync();
        }

        private async void ListViewCategories_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (viewModel.Modified)
            {
                await SaveWallet();
            }

            var category = (Category)e.ClickedItem;
            var pagePayload = new PagePayload() { Dto = category };
            Frame.Navigate(typeof(CategoryPage), pagePayload);
        }

        private async void ListViewBanks_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (viewModel.Modified)
            {
                await SaveWallet();
            }

            var bank = (Bank)e.ClickedItem;
            var pagePayload = new PagePayload() { Dto = bank };
            Frame.Navigate(typeof(BankPage), pagePayload);
        }

        private async void SaveAppBarButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await SaveWallet();
        }

        private async void CancelAppBarButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await viewModel.DiscardChangesAsync();
        }

        private async void DeleteAppBarButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await ShowDialog(resources.GetString("Wallet_DeleteWalletDialog"), viewModel.DeleteWalletCommand.Execute);
            await ReloadMenu();

            if ( viewModel.Wallet == null && Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private async void IconButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (viewModel.Modified)
            {
                await SaveWallet();
            }

            Frame.Navigate(typeof(ImagesPage), viewModel.Image);
        }

        private async void AddCategoryButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (viewModel.Modified)
            {
                await SaveWallet();
            }

            var pagePayload = new PagePayload() { Dto = viewModel.Wallet };
            Frame.Navigate(typeof(CategoriesPage), pagePayload);
        }

        private async void AddBankButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (viewModel.Modified)
            {
                await SaveWallet();
            }

            var pagePayload = new PagePayload() { Dto = viewModel.Wallet };
            Frame.Navigate(typeof(BanksPage), pagePayload);
        }

        private async void DetacheCategoryButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var button = sender as Button;
            var category = button.DataContext as Category;
            await viewModel.DetachCategoryAsync(category);
        }

        private async void DetacheBankButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var button = sender as Button;
            var bank = button.DataContext as Bank;
            await viewModel.DetachBankAsync(bank);
        }

        private async Task SaveWallet()
        {
            await viewModel.SaveWalletAsync();
            await ReloadMenu();
        }

        private async Task ReloadMenu()
        {
            await MenuHelper.ReloadData();
        }
    }
}
