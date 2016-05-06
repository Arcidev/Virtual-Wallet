using BL.Models;
using BL.Service;
using VirtualWallet.ViewModels;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;

namespace VirtualWallet.Pages
{
    public sealed partial class BankPage : Page
    {
        private BankPageViewModel viewModel;
        private double screenWidth;

        public BankPage()
        {
            this.InitializeComponent();
            viewModel = new BankPageViewModel(new BankAccountInfoService(), new TransactionService(), new CategoryService(), ResourceLoader.GetForCurrentView());
            viewModel.BeforeSync = () => IconRotation.Begin();
            viewModel.AfterSync = () => IconRotation.Stop();
            this.DataContext = viewModel;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
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

        private void SettingsAppBarButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SettingsPage));
        }

        private void Page_SizeChanged(object sender, Windows.UI.Xaml.SizeChangedEventArgs e)
        {
            if (screenWidth == e.NewSize.Width)
                return;

            screenWidth = e.NewSize.Width;
            RecalculateLineGraphInterval();
        }

        private void TransactionsLineSeries_DataContextChanged(Windows.UI.Xaml.FrameworkElement sender, Windows.UI.Xaml.DataContextChangedEventArgs args)
        {
            RecalculateLineGraphInterval();
        }

        private void RecalculateLineGraphInterval()
        {
            if (viewModel.Transactions?.Count > 0)
            {
                var size = screenWidth / 200;
                TransactionsLineSeries.IndependentAxis = new DateTimeAxis()
                {
                    Orientation = AxisOrientation.X,
                    IntervalType = DateTimeIntervalType.Days,
                    Interval = viewModel.Transactions.Count / size
                };
            }
        }
    }
}
