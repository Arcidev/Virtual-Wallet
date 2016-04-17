using BL.Models;
using DAL.DataAccess;
using Shared.Filters;

namespace BL.Service
{
    public class IconService : BaseGetService<Icon, DAL.Data.Icon, Icons, IconFilter>, IIconService
    {
    }
}
