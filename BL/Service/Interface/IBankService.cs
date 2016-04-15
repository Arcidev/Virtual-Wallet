using BL.Models.BankModels;
using Shared.Filters;
using Shared.Modifiers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Service
{
    public interface IBankService : IGet<Bank, BankFilter>
    {
        Task<IList<Bank>> GetAll(BankModifier modifier);

        Task<IList<Bank>> Get(BankFilter filter, BankModifier modifier);

        Task<Bank> Get(int id, BankModifier modifier);
    }
}
