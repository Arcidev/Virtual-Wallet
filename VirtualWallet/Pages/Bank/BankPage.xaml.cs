using BL.Models;
using BL.Service;
using Shared.Formatters;
using System;
using System.Linq;
using VirtualWallet.Helpers;
using VirtualWallet.ViewModels;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;

namespace VirtualWallet.Pages
{
    public sealed partial class BankPage : Page
    {
        private readonly BankPageViewModel viewModel;
        private double screenWidth;
        private readonly ResourceLoader resources;

        public BankPage()
        {
            resources = ResourceLoader.GetForCurrentView();
            InitializeComponent();
            viewModel = new BankPageViewModel(new BankAccountInfoService(), new TransactionService(), new CategoryService(), ResourceLoader.GetForCurrentView())
            {
                BeforeSync = () =>
                {
                    IconRotation.Begin();
                    TransactionsAccordion.UnselectAll();
                },
                AfterSync = () =>
                {
                    IconRotation.Stop();
                    RecalculateLineGraphInterval();
                    SetStyles();
                }
            };
            DataContext = viewModel;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            MenuHelper.SetHeader(resources.GetString("Bank_PageTitle"));

            await viewModel.LoadDataAsync((Bank)e.Parameter);
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            viewModel.Dispose();
            base.OnNavigatingFrom(e);
        }

        private void EditAppBarButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(BankCredentialsPage), viewModel.Bank);
        }

        private void Page_SizeChanged(object sender, Windows.UI.Xaml.SizeChangedEventArgs e)
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
                var firstDay = viewModel.Balances?.Last<Tuple<DateTime, double>>();
                var lastDay = viewModel.Balances?.First<Tuple<DateTime, double>>();

                int daysBetween = 10;
                if (firstDay != null && lastDay != null)
                {
                    daysBetween = Math.Max((lastDay.Item1 - firstDay.Item1).Days, daysBetween);
                }

                var size = screenWidth / 130 - 0.5;
                var interval = daysBetween / Math.Min(size, 10);

                TransactionsLineSeries.IndependentAxis = new DateTimeAxis()
                {
                    Orientation = AxisOrientation.X,
                    IntervalType = DateTimeIntervalType.Days,
                    Interval = interval
                };
            }
        }

        private void SetStyles()
        {
            if (string.IsNullOrWhiteSpace(viewModel.BankAccountInfo?.Currency))
                return;

            Style datapointStyle = new Style(typeof(DataPoint));
            datapointStyle.Setters.Add(new Setter(DataPoint.DependentValueStringFormatProperty, CurrencyFormatter.GetFormatter(viewModel.BankAccountInfo.Currency)));
            TransactionsLineSeries.DataPointStyle = datapointStyle;
        }
    }
}
