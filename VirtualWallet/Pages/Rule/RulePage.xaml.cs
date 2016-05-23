using BL.Models;
using BL.Service;
using BL.Service.Menu;
using Shared.Enums;
using System;
using System.Linq;
using VirtualWallet.ViewModels;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace VirtualWallet.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RulePage : Page
    {
        private RulePageViewModel viewModel;
        private ResourceLoader resources;

        public RulePage()
        {
            resources = ResourceLoader.GetForCurrentView();
            this.InitializeComponent();
            viewModel = new RulePageViewModel(new RuleService());
            this.DataContext = viewModel;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            MenuUnil.setHeader("Rule_PageTitle");

            var pagePayload = (PagePayload)e.Parameter;
            var category = (Category)pagePayload?.Dto;

            if (category != null)
            {
                viewModel.CategoryId = category.Id;
            }

            var rule = pagePayload?.Rule;

            if (rule != null)
            {
                viewModel.Rule.Id = rule.Id;
            }
            
            PatternTypeCombobox.ItemsSource = Enum.GetValues(typeof(PatternType)).Cast<PatternType>();

            await viewModel.LoadDataAsync();
            base.OnNavigatedTo(e);
        }

        private async void SaveAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            await viewModel.SaveRuleAsync();

            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private async void UndoAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            await viewModel.DiscardChangesAsync();
        }

        private async void DeleteAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            await viewModel.DeleteRuleAsync();

            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }
    }
}
