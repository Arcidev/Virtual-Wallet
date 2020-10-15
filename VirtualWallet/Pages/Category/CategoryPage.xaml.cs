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
    public sealed partial class CategoryPage : Page
    {
        private readonly CategoryPageViewModel viewModel;
        private PagePayload pagePayload;
        private readonly ResourceLoader resources;

        public CategoryPage()
        {
            InitializeComponent();
            resources = ResourceLoader.GetForCurrentView();
            viewModel = new CategoryPageViewModel(new CategoryService(), new WalletCategoryService(), new RuleService());
            DataContext = viewModel;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            MenuHelper.SetHeader(resources.GetString("Category_PageTitle"));

            pagePayload = (PagePayload)e.Parameter;
            viewModel.Category = (Category)pagePayload.Dto;
            await viewModel.LoadDataAsync();

            if (pagePayload.NewImage != null)
            {
                viewModel.Image = pagePayload.NewImage;
            }

            base.OnNavigatedTo(e);
        }

        protected override async void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            if (viewModel.Modified)
            {
                await viewModel.SaveCategoryAsync();
            }
        }

        private async Task ShowDialog(string message, UICommandInvokedHandler yesHandler)
        {
            var dialog = new MessageDialog(message);
            dialog.Commands.Add(new UICommand(resources.GetString("Dialog_Yes"), yesHandler));
            dialog.Commands.Add(new UICommand(resources.GetString("Dialog_No")));

            await dialog.ShowAsync();
        }

        private void ListViewRules_ItemClick(object sender, ItemClickEventArgs e)
        {
            var rule = (Rule)e.ClickedItem;
            var pagePayload = new PagePayload() { Dto = viewModel.Category, Rule = rule };
            Frame.Navigate(typeof(RulePage), pagePayload);
        }

        private async void SaveAppBarButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await viewModel.SaveCategoryAsync();

            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private async void CancelAppBarButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await viewModel.DiscardChangesAsync();
        }

        private async void DeleteAppBarButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await ShowDialog(resources.GetString("Category_DeleteCategoryDialog"), viewModel.DeleCategoryCommand.Execute);

            if ( viewModel.Category == null && Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private void IconButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ImagesPage), viewModel.Image);
        }

        private void AddRuleButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            //pagePayload.Dto = viewModel.Category;
            var pagePayload = new PagePayload() { Dto = viewModel.Category };
            Frame.Navigate(typeof(RulePage), pagePayload);
        }

        private async void RemoveRuleButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var button = sender as Button;
            var rule = button.DataContext as Rule;
            await viewModel.DetachRuleAsync(rule);
        }
    }
}
