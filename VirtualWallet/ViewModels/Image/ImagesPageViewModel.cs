using BL.Models;
using BL.Service;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualWallet.ViewModels
{
    public class ImagesPageViewModel : ViewModelBase
    {
        private readonly IImageService imageService;
        private Image selectedImage;
        private Image originalImage;
        private List<Image> images;

        public ImagesPageViewModel(IImageService imageService)
        {
            this.imageService = imageService;
        }

        public Image OriginalImage
        {
            get => originalImage;
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
            get => selectedImage;
            set
            {
                if (selectedImage == value)
                    return;

                selectedImage = value;
                NotifyPropertyChanged();
            }
        }

        public List<Image> Images
        {
            get => images;
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
