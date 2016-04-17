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
        private const string iconStorage = "ms-appx:///Assets/Icons/";

        private static readonly Type[] tables = new Type[] 
        {
            typeof(Category),
            typeof(Icon),
            typeof(Bank)
        };

        public async Task InitAsync()
        {
            // await Windows.Storage.ApplicationData.Current.ClearAsync();
            var connection = ConnectionHelper.GetDbAsyncConnection();
            await connection.CreateTablesAsync(tables);

            await InitData(connection);
        }

        private async Task InitData(SQLiteAsyncConnection connection)
        {
            await connection.InsertOrIgnoreAllAsync(new object[] {
                new Icon() { Id = (int)IconId.Fio, Path = $"{iconStorage}FioIcon.jpg" },
                new Bank() { Id = (int)BankId.Fio, Name = "Fio banka", IconId = (int)IconId.Fio } });
        }
    }
}
