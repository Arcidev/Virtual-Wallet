using DAL.Config;
using System.Threading.Tasks;

namespace BL.Service
{
    public class DatabaseService : IDatabaseService
    {
        private static readonly IDatabase _database = new Database();

        public async Task InitAsync()
        {
            await _database.InitAsync();
        }

        public async Task RemoveAllDataAsync()
        {
            await _database.RemoveAllDataAsync();
        }
    }
}
