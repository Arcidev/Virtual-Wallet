using DAL.Data;
using DAL.Helpers;
using Shared.Enums;
using SQLite.Net.Async;
using System;
using System.Threading.Tasks;

namespace DAL.Config
{
    public class Database : IDatabase
    {
        private const string imageStorage = "ms-appx:///Assets/Images/";

        private static readonly Type[] tables = new Type[] 
        {
            typeof(Category),
            typeof(Bank),
            typeof(Image)
        };

        public async Task InitAsync()
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            await connection.CreateTablesAsync(tables);

            await InitData(connection);
        }

        private async Task InitData(SQLiteAsyncConnection connection)
        {
            await connection.InsertOrReplaceAllAsync(new object[] {
                new Image() { Id = (int)ImageId.Fio, Path= $"{imageStorage}Fio.png" },
                new Bank() { Id = (int)BankId.Fio, Name = "Fio banka", ImageId = (int)ImageId.Fio } });
        }
    }
}
