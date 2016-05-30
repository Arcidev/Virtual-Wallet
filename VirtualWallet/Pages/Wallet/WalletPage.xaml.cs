using BL.Models;
using BL.Service;
using Shared.Enums;
using Shared.Formatters;
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
using WinRTXamlToolkit.Controls.DataVisualization.Charting;

namespace VirtualWallet.Pages
{
    public sealed partial class WalletPage : Page
    {
        private WalletPageViewModel viewModel;
        private double screenWidth;
        private ResourceLoader resources;

        public WalletPage()
        {
            resources = ResourceLoader.GetForCurrentView();
            this.InitializeComponent();
            viewModel = new WalletPageViewModel(new BankAccountInfoService(), new TransactionService(), new CategoryService(), new WalletCategoryService(), new WalletBankService(), new CurrencyService(), ResourceLoader.GetForCurrentView());
            viewModel.BeforeSync = () =>
            {
                IconRotation.Begin();
                TransactionsAccordion.UnselectAll();
            };
            viewModel.AfterSync = () =>
            {
                IconRotation.Stop();
                RecalculateLineGraphInterval();
                SetStyles();
            };
            this.DataContext = viewModel;

            TimeRangeComboboxCat.ItemsSource = Enum.GetValues(typeof(TimeRange)).Cast<TimeRange>();
            TimeRangeComboboxTrans.ItemsSource = Enum.GetValues(typeof(TimeRange)).Cast<TimeRange>();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            MenuHelper.SetHeader(resources.GetString("Wallet_PageTitle"));
            await viewModel.LoadDataAsync((Wallet)e.Parameter);
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            viewModel.Dispose();
            base.OnNavigatingFrom(e);
        }

        private void EditAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            var pagePayload = new PagePayload() { Dto = viewModel.Wallet };
            Frame.Navigate(typeof(WalletEditPage), pagePayload);
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (screenWidth == e.NewSize.Width)
                return;

            screenWidth = e.NewSize.Width;
            RecalculateLineGraphInterval();
        }

        private void TransactionsLineSeries_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            SetStyles();
            RecalculateLineGraphInterval();
        }

        private void RecalculateLineGraphInterval()
        {
            if (viewModel.Balances?.Count > 0)
            {
                var size = screenWidth / 200 - 0.5;
                TransactionsLineSeries.IndependentAxis = new DateTimeAxis()
                {
                    Orientation = AxisOrientation.X,
                    IntervalType = DateTimeIntervalType.Days,
                    Interval = viewModel.Balances.Count / size
                };
            }
        }

        private void SetStyles()
        {
            if (string.IsNullOrWhiteSpace(viewModel.Currency))
                return;

            Style datapointStyle = new Style(typeof(DataPoint));
            datapointStyle.Setters.Add(new Setter(DataPoint.DependentValueStringFormatProperty, CurrencyFormatter.GetFormatter(viewModel.Currency)));
            TransactionsLineSeries.DataPointStyle = datapointStyle;
        }
    }
}
