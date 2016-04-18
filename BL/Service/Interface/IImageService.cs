using BL.Models;
using Shared.Filters;

namespace BL.Service
{
    public interface IImageService : IGetService<Image, BaseFilter>
    {
    }
}
