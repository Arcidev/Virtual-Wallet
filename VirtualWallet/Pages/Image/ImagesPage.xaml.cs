using BL.Models;
using BL.Service;
using Cimbalino.Toolkit.Controls;
using System.Linq;
using VirtualWallet.ViewModels;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace VirtualWallet.Pages
{
    public sealed partial class ImagesPage : Page
    {
        private ImagesPageViewModel viewModel;
        private ResourceLoader resources;

        public ImagesPage()
        {
            resources = ResourceLoader.GetForCurrentView();
            this.InitializeComponent();
            viewModel = new ImagesPageViewModel(new ImageService());
            this.DataContext = viewModel;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            this.setPageHeader();

            viewModel.OriginalImage = (BL.Models.Image)e.Parameter;
            await viewModel.LoadDataAsync();
            base.OnNavigatedTo(e);
        }

        private void AcceptAppBarButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var lastPage = Frame.BackStack.Last();
            var pagePayload = (PagePayload) lastPage.Parameter;

            pagePayload.NewImage = viewModel.SelectedImage;

            if (Frame.Navigate(lastPage.SourcePageType, pagePayload))
            {
                Frame.BackStack.RemoveAt(Frame.BackStack.Count - 1);
                Frame.BackStack.RemoveAt(Frame.BackStack.Count - 1);
            }
        }

        private void CancelAppBarButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
                Frame.GoBack();
        }

        private void setPageHeader()
        {
            var rootFrame = Window.Current.Content as HamburgerFrame;
            var header = rootFrame.Header as HamburgerTitleBar;
            header.Title = resources.GetString("Images_PageTitle");
        }
    }
}
