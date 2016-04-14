using DAL.Data;
using DAL.Helpers;
using Shared.Filters;
using SQLite.Net.Async;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public abstract class BaseDataAccess<T> where T : class, IData, new()
    {
        protected AsyncTableQuery<T> ApplyBaseFilters(AsyncTableQuery<T> query, BaseFilter filter)
        {
            if (filter.Ids != null && filter.Ids.Any())
                query = query.Where(x => filter.Ids.Contains(x.Id));

            if (filter.Skip > 0)
                query = query.Skip(filter.Skip.Value);

            if (filter.Take > 0)
                query = query.Take(filter.Take.Value);

            return query;
        }

        public async Task<T> Get(int id)
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            return await connection.Table<T>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task Create(params T[] entities)
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            await connection.InsertAllAsync(entities);
        }

        public async Task Update(params T[] entities)
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            await connection.UpdateAllAsync(entities);
        }

        public async Task Delete(int id)
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            await connection.DeleteAsync(new T() { Id = id });
        }

        public async Task DeleteAll()
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            await connection.DeleteAllAsync<T>();
        }
    }
}
