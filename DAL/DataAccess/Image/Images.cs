using DAL.Data;
using Shared.Filters;

namespace DAL.DataAccess
{
    public class Images : BaseGetDataAccess<Image, BaseFilter>, IImages
    {
    }
}
