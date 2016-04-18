using DAL.Data;
using Shared.Filters;

namespace DAL.DataAccess
{
    public interface IImages : IGet<Image, BaseFilter>
    {
    }
}
