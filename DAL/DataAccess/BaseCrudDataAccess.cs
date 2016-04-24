using DAL.Data;
using DAL.Helpers;
using Shared.Filters;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public abstract class BaseCrudDataAccess<T1, T2> : BaseGetDataAccess<T1, T2>, ICrud<T1, T2> where T1 : class, IDao, new() where T2 : BaseFilter, new()
    {
        public async Task CreateAsync(params T1[] entities)
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            await connection.InsertAllAsync(entities);
        }

        public async Task UpdateAsync(params T1[] entities)
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            await connection.UpdateAllAsync(entities);
        }

        public async Task ReplaceAsync(params T1[] entities)
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            await connection.InsertOrReplaceAllAsync(entities);
        }

        public async Task DeleteAsync(int id)
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            await connection.DeleteAsync(new T1() { Id = id });
        }

        public async Task DeleteAllAsync()
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            await connection.DeleteAllAsync<T1>();
        }
    }
}
