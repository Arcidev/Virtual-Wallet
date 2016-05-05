using BL.Models;
using Shared.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Service
{
    public interface ITransactionService : ICrudService<Transaction, TransactionFilter>
    {
        /// <summary>
        /// Gets transactions by bankId
        /// </summary>
        /// <param name="bankId">Id of bank</param>
        /// <returns>List of all bank transactions</returns>
        Task<IList<Transaction>> GetByBankIdAsync(int? bankId, TransactionFilter filter = null);
    }
}
