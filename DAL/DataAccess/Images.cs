using DAL.Data;
using Shared.Filters;
using SQLite.Net.Async;

namespace DAL.DataAccess
{
    public class Images : BaseGetDataAccess<Image, BaseFilter>, IImages
    {
        protected override AsyncTableQuery<Image> ApplyFilters(AsyncTableQuery<Image> query, BaseFilter filter)
        {
            return query;
        }
    }
}
