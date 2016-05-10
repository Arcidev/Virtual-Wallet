using BL.Models;
using DAL.DataAccess;
using Shared.Filters;
using Shared.Modifiers;

namespace BL.Service
{
    public class WalletBankService : BaseModifiableCrudService<WalletBank, DAL.Data.WalletBank, WalletsBanks, WalletBankFilter, WalletBankModifier>, IWalletBankService
    {
    }
}
