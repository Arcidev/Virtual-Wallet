using DAL.Data;
using Shared.Filters;
using Shared.Modifiers;
using SQLite.Net.Async;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public class WalletsBanks : BaseModifiableCrudDataAccess<WalletBank, WalletBankFilter, WalletBankModifier>, IWalletsBanks
    {
        private static readonly IWallets wallets = new Wallets();
        private static readonly IBanks categories = new Banks();

        protected async override Task ApplyModifiersAsync(WalletBank walletBank, WalletBankModifier modifier)
        {
            if (modifier.IncludeWallet || modifier.IncludeAll)
                walletBank.Wallet = await wallets.GetAsync(walletBank.WalletId, new WalletModifier() { IncludeImage = true });

            if (modifier.IncludeBank || modifier.IncludeAll)
                walletBank.Bank = await categories.GetAsync(walletBank.BankId, new BankModifier() { IncludeImage = true });

        }

        protected override AsyncTableQuery<WalletBank> ApplyFilters(AsyncTableQuery<WalletBank> query, WalletBankFilter filter)
        {
            if (filter.WalletId.HasValue && filter.BankId.HasValue)
                query = query.Where(x => x.WalletId.Equals(filter.WalletId) && x.BankId.Equals(filter.BankId));

            else if (filter.WalletId.HasValue)
                query = query.Where(x => x.WalletId.Equals(filter.WalletId));

            else if (filter.BankId.HasValue)
                query = query.Where(x => x.BankId.Equals(filter.BankId));

            return base.ApplyFilters(query, filter);
        }
    }
}
