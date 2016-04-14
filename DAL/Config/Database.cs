using DAL.Data;
using DAL.Helpers;
using System.Threading.Tasks;

namespace DAL.Config
{
    public class Database : IDatabase
    {
        public async Task InitAsync()
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            await connection.CreateTablesAsync(typeof(Category), typeof(Icon));
        }
    }
}
