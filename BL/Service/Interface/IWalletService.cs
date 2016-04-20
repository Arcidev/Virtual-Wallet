using BL.Models;
using Shared.Filters;
using Shared.Modifiers;

namespace BL.Service
{
    public interface IWalletService : IModifiableGetService<Wallet, WalletFilter, WalletModifier> 
    {
    }
}
