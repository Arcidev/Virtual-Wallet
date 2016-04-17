using BL.Models;
using Shared.Filters;

namespace BL.Service
{
    public interface IIconService : IGetService<Icon, IconFilter>
    {
    }
}
