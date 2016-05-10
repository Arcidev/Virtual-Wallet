using DAL.Data;
using Shared.Filters;
using Shared.Modifiers;

namespace DAL.DataAccess
{
    public interface IWalletsBanks : IModifiableCrud<WalletBank, WalletBankFilter, WalletBankModifier>
    {
    }
}
