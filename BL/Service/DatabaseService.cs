using DAL.Config;
using Shared.Enums;
using System.Threading.Tasks;

namespace BL.Service
{
    public class DatabaseService : IDatabaseService
    {
        private static readonly IDatabase _database = new Database();

        public async Task<CopyDatabaseResult> RetrieveFromRoamingFolderAsync()
        {
            return await _database.RetrieveFromRoamingFolder();
        }

        public async Task<CopyDatabaseResult> CopyToRoamingFolderAsync()
        {
            return await _database.CopyToRoamingFolder();
        }

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
