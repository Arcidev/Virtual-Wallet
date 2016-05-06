using DAL.Data;
using SQLite.Net.Async;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Helpers
{
    internal class InitDataDevHelper
    {
        // Just for testing
        public static async Task InitTempData(SQLiteAsyncConnection connection)
        {
            await connection.DeleteAllAsync<WalletCategory>();
            await connection.DeleteAllAsync<Wallet>();

            var wallet = new Wallet() { Name = "Test wallet", ImageId = 15 };
            await connection.InsertAllAsync(new object[]
            {
                    wallet
            });

            Category category = (await connection.Table<Category>().ToListAsync()).FirstOrDefault();
            if (category == null)
            {
                category = new Category() { Name = "Test category", ImageId = 139 };
                await connection.InsertAllAsync(new object[]
                {
                    category
                });
            }

            await connection.InsertAllAsync(new object[]
            {
                    new WalletCategory() { WalletId = wallet.Id, CategoryId = category.Id }
            });
        }
    }
}
