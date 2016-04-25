using DAL.Data;
using Shared.Filters;
using Shared.Modifiers;
using SQLite.Net.Async;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public class WalletsCategories : BaseModifiableCrudDataAccess<WalletCategory, WalletCategoryFilter, WalletCategoryModifier>, IWalletsCategories
    {
        private static readonly IWallets wallets = new Wallets();
        private static readonly ICategories categories = new Categories();

        protected async override Task ApplyModifiersAsync(WalletCategory walletCategory, WalletCategoryModifier modifier)
        {
            if (modifier.IncludeWallet || modifier.IncludeAll)
                walletCategory.Wallet = await wallets.GetAsync(walletCategory.WalletId, new WalletModifier() { IncludeImage = true });

            if (modifier.IncludeCategory || modifier.IncludeAll)
                walletCategory.Category = await categories.GetAsync(walletCategory.CategoryId, new CategoryModifier() { IncludeImage = true });

        }

        protected override AsyncTableQuery<WalletCategory> ApplyFilters(AsyncTableQuery<WalletCategory> query, WalletCategoryFilter filter)
        {
            if (filter.WalletId.HasValue && filter.CategoryId.HasValue)
                query = query.Where(x => x.WalletId.Equals(filter.WalletId) && x.CategoryId.Equals(filter.CategoryId));

            else if (filter.WalletId.HasValue)
                query = query.Where(x => x.WalletId.Equals(filter.WalletId));

            else if (filter.CategoryId.HasValue)
                query = query.Where(x => x.CategoryId.Equals(filter.CategoryId));

            return query;
        }
    }
}
