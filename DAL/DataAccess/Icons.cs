using DAL.Data;
using DAL.Helpers;
using System.Collections.Generic;
using Shared.Filters;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public class Icons : BaseDataAccess<Icon>, IIcons
    {
        public async Task<IList<Icon>> GetAll()
        {
            return await Get(null);
        }

        public async Task<IList<Icon>> Get(IconFilter filter = null)
        {
            if (filter == null)
                filter = new IconFilter();

            var connection = ConnectionHelper.GetDbAsyncConnection();
            var query = connection.Table<Icon>();

            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(x => x.Name.Contains(filter.Name));

            return await ApplyBaseFilters(query, filter).ToListAsync();
        }
    }
}
