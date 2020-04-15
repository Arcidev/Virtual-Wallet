using System.Collections.Generic;
using DAL.Data;
using Shared.Filters;
using DAL.Helpers;
using System.Threading.Tasks;
using SQLite;

namespace DAL.DataAccess
{
    public class Transactions : BaseCrudDataAccess<Transaction, TransactionFilter>, ITransactions
    {
        public async Task<List<Transaction>> GetByBankIdAsync(int? bankId, TransactionFilter filter = null)
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            var query = connection.Table<Transaction>().Where(x => x.BankId == bankId);

            return await ApplyFilters(query, filter ?? new TransactionFilter()).ToListAsync();
        }

        protected override AsyncTableQuery<Transaction> ApplyFilters(AsyncTableQuery<Transaction> query, TransactionFilter filter)
        {
            if (filter.DateSince.HasValue)
                query = query.Where(x => x.Date >= filter.DateSince.Value);

            if (filter.IsCashPayment)
                query = query.Where(x => !x.BankId.HasValue);

            return base.ApplyFilters(query, filter);
        }
    }
}
