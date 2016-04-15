using DAL.Data;
using DAL.Helpers;
using Shared.Filters;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public abstract class BaseCrudDataAccess<T1, T2> : BaseGetDataAccess<T1, T2> where T1 : class, IDao, new() where T2 : BaseFilter, new()
    {
        public async Task Create(params T1[] entities)
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            await connection.InsertAllAsync(entities);
        }

        public async Task Update(params T1[] entities)
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            await connection.UpdateAllAsync(entities);
        }

        public async Task Delete(int id)
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            await connection.DeleteAsync(new T1() { Id = id });
        }

        public async Task DeleteAll()
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            await connection.DeleteAllAsync<T1>();
        }
    }
}
