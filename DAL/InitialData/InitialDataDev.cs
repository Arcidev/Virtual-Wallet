using DAL.Data;
using SQLite.Net.Async;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.InitialData
{
    class InitialDataDev
    {
        // Just for testing
        public static async Task InitTempData(SQLiteAsyncConnection connection)
        {
            // Prevent this to multiply on every start of app
            if (!(await connection.Table<Wallet>().ToListAsync()).Any())
            {
                await connection.InsertAllAsync(new object[]
                {
                    new Wallet() { Id = 1,  Name = "Test wallet", ImageId = 15 }
                });
            }

            if (!(await connection.Table<Category>().ToListAsync()).Any())
            {
                await connection.InsertAllAsync(new object[]
                {
                    new Category() { Id = 1, Name = "Test category", ImageId = 139 }
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
