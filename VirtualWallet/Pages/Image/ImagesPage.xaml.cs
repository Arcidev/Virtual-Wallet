using BL.Service;
using System.Linq;
using VirtualWallet.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace VirtualWallet.Pages
{
    public sealed partial class ImagesPage : Page
    {
        private ImagesPageViewModel viewModel;

        public ImagesPage()
        {
            this.InitializeComponent();
            viewModel = new ImagesPageViewModel(new ImageService());
            this.DataContext = viewModel;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            viewModel.OriginalImage = (BL.Models.Image)e.Parameter; ;
            await viewModel.LoadDataAsync();
            base.OnNavigatedTo(e);
        }

        private void AcceptAppBarButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var lastPage = Frame.BackStack.Last();
            var pagePayload = (PagePayload) lastPage.Parameter;

            pagePayload.NewImage = viewModel.SelectedImage;

            Frame.GoBack();
        }

        private void CancelAppBarButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
