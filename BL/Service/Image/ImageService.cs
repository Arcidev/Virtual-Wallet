using BL.Models;
using DAL.DataAccess;
using Shared.Filters;

namespace BL.Service
{
    public class ImageService : BaseGetService<Image, DAL.Data.Image, Images, BaseFilter>, IImageService
    {
    }
}
