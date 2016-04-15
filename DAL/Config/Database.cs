using DAL.Data;
using DAL.Helpers;
using Shared.Enums;
using SQLite.Net.Async;
using System.Threading.Tasks;

namespace DAL.Config
{
    public class Database : IDatabase
    {
        public async Task InitAsync()
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            await connection.CreateTablesAsync(
                typeof(Category),
                typeof(Icon),
                typeof(Bank));

            await InitData(connection);
        }

        private async Task InitData(SQLiteAsyncConnection connection)
        {
            await connection.InsertAllAsync(new[] {
                new Bank() { Id = (int)BankId.Fio, Name = "Fio", IconId = null } });
        }
    }
}
