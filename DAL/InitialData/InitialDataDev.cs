using DAL.Data;
using SQLite.Net.Async;
using System.Threading.Tasks;

namespace DAL.InitialData
{
    internal class InitialDataDev
    {
        // Just for testing
        public static async Task InitTempData(SQLiteAsyncConnection connection)
        {
            await connection.DeleteAllAsync<CategoryRule>();
            await connection.DeleteAllAsync<Rule>();
            await connection.DeleteAllAsync<WalletCategory>();
            await connection.DeleteAllAsync<Category>();
            await connection.DeleteAllAsync<Wallet>();

            await connection.InsertAllAsync(new object[]
            {
                    new Wallet() { Id = 1,  Name = "Test wallet", ImageId = 15 }
            });

            await connection.InsertAllAsync(new object[]
            {
                    new Category() { Id = 1, Name = "Test category", ImageId = 139 }
            });

            await connection.InsertAllAsync(new object[]
            {
                    new WalletCategory() { WalletId = 1, CategoryId = 1 }
            });

            await connection.InsertAllAsync(new object[]
            {
                    new Rule() { Id = 1, Name = "Test rule", Description = "This is test rule", Pattern = "*" }
            });

            await connection.InsertAllAsync(new object[]
            {
                    new CategoryRule() { RuleId = 1, CategoryId = 1 }
            });
        }
    }
}
