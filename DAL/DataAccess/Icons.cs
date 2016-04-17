using DAL.Data;
using Shared.Filters;
using SQLite.Net.Async;

namespace DAL.DataAccess
{
    public class Icons : BaseGetDataAccess<Icon, IconFilter>, IIcons
    {
        protected override AsyncTableQuery<Icon> ApplyFilters(AsyncTableQuery<Icon> query, IconFilter filter)
        {
            return query;
        }
    }
}
