using DAL.Data;
using DAL.Helpers;
using Shared.Filters;
using SQLite.Net.Async;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public abstract class BaseCrudDataAccess<T1, T2> : BaseGetDataAccess<T1, T2>, ICrud<T1, T2> where T1 : class, IDao, new() where T2 : BaseFilter, new()
    {
        public async Task InsertAsync(T1 entity)
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            await connection.InsertAsync(entity);
        }

        public async Task InsertAsync(params T1[] entities)
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            await connection.InsertAllAsync(entities);
        }

        public async Task InsertOrIgnoreAsync(T1 entity)
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            await connection.InsertOrIgnoreAsync(entity);
        }

        public async Task InsertOrIgnoreAsync(params T1[] entities)
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            await connection.InsertOrIgnoreAllAsync(entities);
        }

        public async Task InsertOrReplaceAsync(T1 entity)
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            await connection.InsertOrReplaceAsync(entity);
        }

        public async Task InsertOrReplaceAsync(params T1[] entities)
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            await connection.InsertOrReplaceAllAsync(entities);
        }

        public async Task UpdateAsync(params T1[] entities)
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            await connection.UpdateAllAsync(entities);
        }

        public async Task DeleteAsync(int id)
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            await connection.DeleteAsync(new T1() { Id = id });
            await OnEntityDeletedAsync(connection, id);
        }

        public async Task DeleteAllAsync()
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            await connection.DeleteAllAsync<T1>();
        }

        protected virtual Task OnEntityDeletedAsync(SQLiteAsyncConnection connection, int id)
        {
            return Task.FromResult(true);
        }
    }
}
