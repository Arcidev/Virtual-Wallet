using BL.Models;
using Shared.Filters;
using Shared.Modifiers;

namespace BL.Service
{
    public interface IWalletBankService : IModifiableCrudService<WalletBank, WalletBankFilter, WalletBankModifier>
    {
    }
}
