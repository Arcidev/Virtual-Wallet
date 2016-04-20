using DAL.Data;
using Shared.Filters;
using Shared.Modifiers;

namespace DAL.DataAccess
{
    public interface IWallets : IModifiableGet<Wallet, WalletFilter, WalletModifier>
    {
    }
}
