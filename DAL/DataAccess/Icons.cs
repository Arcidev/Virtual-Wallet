using DAL.Data;
using DAL.Helpers;
using System.Collections.Generic;
using Shared.Filters;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public class Icons : BaseDataAccess, IIcons
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

        public async Task<Icon> Get(int id)
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            return await connection.Table<Icon>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task Create(params Icon[] icons)
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            await connection.InsertAllAsync(icons);
        }

        public async Task Update(params Icon[] icons)
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            await connection.UpdateAllAsync(icons);
        }

        public async Task Delete(int id)
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            await connection.DeleteAsync(new Icon() { Id = id });
        }

        public async Task DeleteAll()
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            await connection.DeleteAllAsync<Icon>();
        }
    }
}
