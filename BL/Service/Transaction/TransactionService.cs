using System.Collections.Generic;
using System.Threading.Tasks;
using BL.Models;
using DAL.DataAccess;
using Shared.Filters;
using Mapster;

namespace BL.Service
{
    public class TransactionService : BaseCrudService<Transaction, DAL.Data.Transaction, Transactions, TransactionFilter>, ITransactionService
    {
        public async Task<List<Transaction>> GetByBankIdAsync(int? bankId, TransactionFilter filter = null)
        {
            return (await _instance.GetByBankIdAsync(bankId, filter)).Adapt<List<Transaction>>();
        }
    }
}
