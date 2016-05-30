using DAL.Data;
using DAL.Helpers;
using Shared.Enums;
using SQLite.Net.Async;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace DAL.Config
{
    public class Database : IDatabase
    {
        private static readonly Type[] tables = new Type[]
        {
            typeof(Rule),
            typeof(Category),
            typeof(Bank),
            typeof(Image),
            typeof(Wallet),
            typeof(WalletCategory),
            typeof(WalletBank),
            typeof(BankAccountInfo),
            typeof(Transaction),
            typeof(Currency),
        };

        public async Task InitAsync()
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            await connection.CreateTablesAsync(tables);

            await InitData(connection);
        }

        public async Task RemoveAllDataAsync()
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            await connection.DeleteAllAsync<Rule>();
            await connection.DeleteAllAsync<WalletCategory>();
            await connection.DeleteAllAsync<WalletBank>();
            await connection.DeleteAllAsync<Category>();
            await connection.DeleteAllAsync<Wallet>();
            await connection.DeleteAllAsync<BankAccountInfo>();
        }

        public async Task<CopyDatabaseResult> CopyToRoamingFolder()
        {
            if (!File.Exists(ConnectionHelper.DbFilePath))
                return CopyDatabaseResult.FileNotFound;

            var dbFile = await StorageFile.GetFileFromPathAsync(ConnectionHelper.DbFilePath);
            await dbFile.CopyAsync(ApplicationData.Current.RoamingFolder, ConnectionHelper.DbFileName, NameCollisionOption.ReplaceExisting);

            return CopyDatabaseResult.Success;
        }

        public async Task<CopyDatabaseResult> RetrieveFromRoamingFolder()
        {
            var file = Path.Combine(ApplicationData.Current.RoamingFolder.Path, ConnectionHelper.DbFileName);
            if (!File.Exists(file))
                return CopyDatabaseResult.FileNotFound;

            var dbFile = await StorageFile.GetFileFromPathAsync(file);
            await dbFile.CopyAsync(ApplicationData.Current.LocalFolder, ConnectionHelper.DbFileName, NameCollisionOption.ReplaceExisting);

            return CopyDatabaseResult.Success;
        }

        private async Task InitData(SQLiteAsyncConnection connection)
        {
            await InitDataHelper.InitData(connection);
        }
    }
}
