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

        private void GridViewIcons_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private async void AcceptAppBarButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            
            var lastPage = Frame.BackStack.Last();

            //dto.Image = viewModel.SelectedImage;

            var pagePayload = (BL.Models.PagePayload) lastPage.Parameter;

            pagePayload.NewImage = viewModel.SelectedImage;

            Frame.GoBack();
            //Frame.Navigate(lastPage, dto);
        }

        private async void CancelAppBarButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.GoBack();
            //var lastPage = Frame.BackStack.Last().SourcePageType;
            //dto.Image = viewModel.OriginalImage;
            //Frame.Navigate(lastPage, dto);
        }



    }
}
