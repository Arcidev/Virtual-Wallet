using BL.Models;
using DAL.DataAccess;
using Shared.Filters;

namespace BL.Service
{
    public class IconService : BaseCrudService<Icon, DAL.Data.Icon, Icons, IconFilter>, IIconService
    {
    }
}
