using DAL.Data;
using Shared.Filters;
using SQLite.Net.Async;

namespace DAL.DataAccess
{
    public class Icons : BaseCrudDataAccess<Icon, IconFilter>, IIcons
    {
        protected override AsyncTableQuery<Icon> ApplyFilters(AsyncTableQuery<Icon> query, IconFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(x => x.Name.Contains(filter.Name));

            return query;
        }
    }
}
