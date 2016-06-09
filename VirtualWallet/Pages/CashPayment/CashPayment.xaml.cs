using BL.Models;
using BL.Service;
using Shared.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;
using VirtualWallet.Helpers;
using VirtualWallet.ViewModels;
using Windows.ApplicationModel.Resources;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
            TimeRangeCombobox.ItemsSource = Enum.GetValues(typeof(TimeRange)).Cast<TimeRange>();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await viewModel.LoadDataAsync();
            MenuHelper.SetHeader(resources.GetString("CashPayment_PageTitle"));
            base.OnNavigatedTo(e);
        }

        private async void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await viewModel.CreateTransaction();
            }
            catch (FormatException)
            {
                await ShowDialog(resources.GetString("CashPayment_AmountFormatException"));
            }
        }

        private async void RemoveTransactionButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var transaction = button.DataContext as Transaction;
            await viewModel.DeleteTransaction(transaction);
        }

        private async Task ShowDialog(string message)
        {
            var dialog = new MessageDialog(message);
            dialog.Commands.Add(new UICommand(resources.GetString("Dialog_Ok")));

            await dialog.ShowAsync();
        }
    }
}
