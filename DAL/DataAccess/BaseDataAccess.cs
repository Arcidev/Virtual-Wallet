using DAL.Data;
using Shared.Filters;
using SQLite.Net.Async;
using System.Linq;

namespace DAL.DataAccess
{
    public abstract class BaseDataAccess
    {
        protected AsyncTableQuery<T> ApplyBaseFilters<T>(AsyncTableQuery<T> query, BaseFilter filter) where T : class, IData
        {
            if (filter.Ids != null && filter.Ids.Any())
                query = query.Where(x => filter.Ids.Contains(x.Id));

            if (filter.Skip > 0)
                query = query.Skip(filter.Skip.Value);

            if (filter.Take > 0)
                query = query.Take(filter.Take.Value);

            return query;
        }
    }
}
