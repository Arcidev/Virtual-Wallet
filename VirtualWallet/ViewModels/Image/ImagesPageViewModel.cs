using BL.Models;
using BL.Service;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualWallet.ViewModels
{
    public class ImagesPageViewModel : ViewModelBase
    {
        private IImageService imageService;
        private Image selectedImage;
        private Image originalImage;
        private IList<Image> images;

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

        public IList<Image> Images
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
            Images = await imageService.GetAllAsync();

            if (OriginalImage != null)
                SelectedImage = Images.FirstOrDefault(x => x.Id == OriginalImage.Id);
        }
    }
}
