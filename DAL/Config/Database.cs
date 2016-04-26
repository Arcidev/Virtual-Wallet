using DAL.Data;
using DAL.Helpers;
using Shared.Enums;
using SQLite.Net.Async;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

namespace DAL.Config
{
    public class Database : IDatabase
    {
        private const string imageStorage = "ms-appx:///Assets/Images/";

        private static readonly Type[] tables = new Type[]
        {
            typeof(Rule),
            typeof(Category),
            typeof(CategoryRule),
            typeof(Bank),
            typeof(Image),
            typeof(Wallet),
            typeof(WalletCategory),
            typeof(BankAccountInfo)
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
            await connection.DeleteAllAsync<CategoryRule>();
            await connection.DeleteAllAsync<Rule>();
            await connection.DeleteAllAsync<WalletCategory>();
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
            await connection.InsertOrReplaceAllAsync(new object[]
            {
                new Image() { Id = (int)ImageId.Fio, Path= $"{imageStorage}Fio.png" },
                new Bank() { Id = (int)BankId.Fio, Name = "Fio banka", ImageId = (int)ImageId.Fio },
                new Image() { Id = (int)ImageId.Wallet, Path= $"{imageStorage}/Wallets/wallet01.png" },
                new Image() { Id = (int)ImageId.Category, Path= $"{imageStorage}/Categories/Transport/Transport1.png" }               
            });

            await InitTempData(connection);
        }

        // Just for testing
        private async Task InitTempData(SQLiteAsyncConnection connection)
        {
            // Prevent this to multiply on every start of app
            if (!(await connection.Table<Wallet>().ToListAsync()).Any())
            {
                await connection.InsertAllAsync(new object[]
                {
                    new Wallet() { Id = 1,  Name = "Test wallet", ImageId = (int)ImageId.Wallet }
                });
            }

            if (!(await connection.Table<Category>().ToListAsync()).Any())
            {
                await connection.InsertAllAsync(new object[]
                {
                    new Category() { Id = 1, Name = "Test category", ImageId = (int)ImageId.Category }
                });
            }

            if (!(await connection.Table<WalletCategory>().ToListAsync()).Any())
            {
                await connection.InsertAllAsync(new object[]
                {
                    new WalletCategory() { WalletId = 1, CategoryId = 1 }
                });
            }

            if (!(await connection.Table<Rule>().ToListAsync()).Any())
            {
                await connection.InsertAllAsync(new object[]
                {
                    new Rule() { Id = 1, Name = "Test rule", Description = "This is test rule", Pattern = "*" }
                });
            }

            if (!(await connection.Table<CategoryRule>().ToListAsync()).Any())
            {
                await connection.InsertAllAsync(new object[]
                {
                    new CategoryRule() { RuleId = 1, CategoryId = 1 }
                });
            }
        }
    }
}
