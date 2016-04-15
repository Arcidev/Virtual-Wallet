using DAL.Data;
using Shared.Filters;
using Shared.Modifiers;

namespace DAL.DataAccess
{
    public interface IBanks : IModifiableGet<Bank, BankFilter, BankModifier>
    {
    }
}
