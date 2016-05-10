
namespace BL.Models
{
    public interface IPagePayload
    {
        IDto Dto { get; set; }

        Image NewImage { get; set; }
    }
}
