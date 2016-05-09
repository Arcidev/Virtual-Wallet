using BL.Models;
using DAL.DataAccess;
using Shared.Filters;
using Shared.Modifiers;

namespace BL.Service
{
    public class WalletService : BaseModifiableCrudService<Wallet, DAL.Data.Wallet, Wallets, WalletFilter, WalletModifier>, IWalletService
    {
    }
}
