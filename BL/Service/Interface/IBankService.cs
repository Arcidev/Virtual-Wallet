using BL.Models;
using Shared.Filters;
using Shared.Modifiers;

namespace BL.Service
{
    public interface IBankService : IModifiableGetService<Bank, BankFilter, BankModifier>
    {
    }
}
