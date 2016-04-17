using DAL.Data;
using Shared.Filters;

namespace DAL.DataAccess
{
    public interface IIcons : IGet<Icon, IconFilter>
    {
    }
}
