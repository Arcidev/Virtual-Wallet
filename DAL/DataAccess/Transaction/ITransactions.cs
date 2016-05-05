using DAL.Data;
using Shared.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public interface ITransactions : ICrud<Transaction, TransactionFilter>
    {
        /// <summary>
        /// Gets transactions by bankId
        /// </summary>
        /// <param name="bankId">Id of bank</param>
        /// <returns>List of all bank transactions</returns>
        Task<IList<Transaction>> GetByBankIdAsync(int? bankId, TransactionFilter filter = null);
    }
}
