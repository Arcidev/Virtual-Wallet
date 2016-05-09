using BL.Models;
using Shared.Filters;
using Shared.Modifiers;

namespace BL.Service
{
    public interface IWalletService : IModifiableCrudService<Wallet, WalletFilter, WalletModifier> 
    {
    }
}
