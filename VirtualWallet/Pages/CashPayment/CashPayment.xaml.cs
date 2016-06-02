using BL.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using VirtualWallet.ViewModels;
using Windows.ApplicationModel.Resources;
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
    public sealed partial class CashPayment : Page
    {
        private CashPaymentViewModel viewModel;
        private ResourceLoader resources;

        public CashPayment()
        {
            this.InitializeComponent();
            resources = ResourceLoader.GetForCurrentView();
            viewModel = new CashPaymentViewModel(new TransactionService(), new CurrencyService(), ResourceLoader.GetForCurrentView());
            this.DataContext = viewModel;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await viewModel.LoadDataAsync();
            base.OnNavigatedTo(e);
        }

        private async void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            await viewModel.CreateTransaction();
        }

        private void ListViewRules_ItemClick(object sender, RoutedEventArgs e)
        {
            //var transaction = (Transaction)e.ClickedItem;
        }

        private void RemoveTransactionButton_Click(object sender, RoutedEventArgs e)
        {
            //var button = sender as Button;
            //var transaction = button.DataContext as Transaction;
        }
    }
}
