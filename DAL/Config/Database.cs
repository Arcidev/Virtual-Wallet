using DAL.Data;
using DAL.Helpers;
using System.Threading.Tasks;

namespace DAL.Config
{
    public class Database : IDatabase
    {
        public async Task InitDatabaseAsync()
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            await connection.CreateTablesAsync(typeof(Category));
        }
    }
}
