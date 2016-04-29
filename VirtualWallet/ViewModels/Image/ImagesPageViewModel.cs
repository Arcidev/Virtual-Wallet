using BL.Models;
using BL.Service;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace VirtualWallet.ViewModels
{
    public class ImagesPageViewModel : ViewModelBase
    {
        private IImageService imageService;
        private Image selectedImage;
        private Image originalImage;
        private ObservableCollection<Image> images;

        public ImagesPageViewModel(IImageService imageService)
        {
            this.imageService = imageService;
        }

        public Image OriginalImage
        {
            get { return originalImage; }
            set
            {
                if (originalImage == value)
                    return;

                originalImage = value;
                NotifyPropertyChanged();
            }
        }

        public Image SelectedImage
        {
            get { return selectedImage; }
            set
            {
                if (selectedImage == value)
                    return;

                selectedImage = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<Image> Images
        {
            get { return images; }
            set
            {
                if (images == value)
                    return;

                images = value;
                NotifyPropertyChanged();
            }
        }

        public async Task LoadDataAsync()
        {
            var images = await imageService.GetAllAsync();
            Images = new ObservableCollection<Image>(images);

            foreach (Image image in Images)
            {
                if (image.Id == OriginalImage.Id)
                {
                    SelectedImage = image;
                }
            }
        }
    }
    
    

    
}
