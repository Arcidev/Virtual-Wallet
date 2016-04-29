using BL.Models;
using Shared.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Service
{
    public interface ITransactionService : ICrudService<Transaction, BaseFilter>
    {
        Task<IList<Transaction>> GetByBankIdAsync(int? bankId);
    }
}
