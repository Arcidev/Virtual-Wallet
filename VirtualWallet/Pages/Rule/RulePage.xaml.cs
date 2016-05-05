﻿using BL.Models;
using BL.Service;
using Shared.Enums;
using System.Linq;
using VirtualWallet.ViewModels;
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
        private PagePayload pagePayload;

        public RulePage()
        {
            this.InitializeComponent();
            viewModel = new RulePageViewModel(new RuleService(), new CategoryRuleService());
            this.DataContext = viewModel;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            pagePayload = (PagePayload)e.Parameter;

            var category = (Category)pagePayload?.Dto;
            viewModel.CategoryId = category?.Id;

            var rule = pagePayload?.Rule;

            if (rule != null)
            {
                viewModel.Rule.Id = rule.Id;
            }
            
            await viewModel.LoadDataAsync();

            PatternTypeCombobox.ItemsSource = PatternType.GetValues(typeof(PatternType)).Cast<PatternType>();

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
