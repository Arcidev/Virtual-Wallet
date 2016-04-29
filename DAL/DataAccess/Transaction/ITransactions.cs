using DAL.Data;
using Shared.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public interface ITransactions : ICrud<Transaction, BaseFilter>
    {
        Task<IList<Transaction>> GetByBankIdAsync(int? bankId);
    }
}
