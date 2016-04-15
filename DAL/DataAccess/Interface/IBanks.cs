using DAL.Data;
using Shared.Filters;
using Shared.Modifiers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public interface IBanks : IGet<Bank, BankFilter>
    {
        Task<IList<Bank>> GetAll(BankModifier modifier);

        Task<IList<Bank>> Get(BankFilter filter, BankModifier modifier);

        Task<Bank> Get(int id, BankModifier modifier);
    }
}
