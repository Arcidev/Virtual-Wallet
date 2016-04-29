using System.Collections.Generic;
using DAL.Data;
using Shared.Filters;
using DAL.Helpers;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public class Transactions : BaseCrudDataAccess<Transaction, BaseFilter>, ITransactions
    {
        public async Task<IList<Transaction>> GetByBankIdAsync(int? bankId)
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            return await connection.Table<Transaction>().Where(x => x.BankId == bankId).ToListAsync();
        }
    }
}
