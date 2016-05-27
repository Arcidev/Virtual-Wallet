using BL.Models;
using BL.Service;
using Shared.Formatters;
using VirtualWallet.Helpers;
using VirtualWallet.ViewModels;
using Windows.ApplicationModel.Resources;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;

namespace VirtualWallet.Pages
{
    public sealed partial class BankPage : Page
    {
        private BankPageViewModel viewModel;
        private double screenWidth;
        private ResourceLoader resources;

        public BankPage()
        {
            resources = ResourceLoader.GetForCurrentView();
            this.InitializeComponent();
            viewModel = new BankPageViewModel(new BankAccountInfoService(), new TransactionService(), new CategoryService(), ResourceLoader.GetForCurrentView());
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
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            MenuHelper.SetHeader(resources.GetString("Bank_PageTitle"));

            Bank bank = (Bank) e.Parameter;

            if (!bank.HasCredentials)
            {
                var dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;
                dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                  {
                      if (Frame.Navigate(typeof(BankCredentialsPage), bank))
                      {
                          Frame.BackStack.RemoveAt(Frame.BackStack.Count - 1);
                      }
                  });
            }
            
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
            if (string.IsNullOrWhiteSpace(viewModel.BankAccountInfo?.Currency))
                return;

            Style datapointStyle = new Style(typeof(DataPoint));
            datapointStyle.Setters.Add(new Setter(DataPoint.DependentValueStringFormatProperty, CurrencyFormatter.GetFormatter(viewModel.BankAccountInfo.Currency)));
            TransactionsLineSeries.DataPointStyle = datapointStyle;
        }
    }
}
