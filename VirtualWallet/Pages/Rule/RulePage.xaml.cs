using BL.Models;
using BL.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using VirtualWallet.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
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

        public RulePage()
        {
            this.InitializeComponent();
            viewModel = new RulePageViewModel(new RuleService(), new CategoryRuleService());
            this.DataContext = viewModel;
        }

        

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var rule = (Rule)e.Parameter;

            if (rule != null)
            {
                viewModel.Rule = rule;
            }
            
            await viewModel.LoadDataAsync();
            
            base.OnNavigatedTo(e);
        }

        private async void SaveAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            await viewModel.SaveRuleAsync();

            var lastPage = Frame.BackStack.Last();
            var pagePayload = (PagePayload)lastPage.Parameter;

            if (pagePayload != null)
            {
                pagePayload.NewRule = viewModel.Rule;
            }

            if (Frame.Navigate(lastPage.SourcePageType, pagePayload))
            {
                this.Frame.BackStack.RemoveAt(this.Frame.BackStack.Count - 1);
                this.Frame.BackStack.RemoveAt(this.Frame.BackStack.Count - 1);
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
