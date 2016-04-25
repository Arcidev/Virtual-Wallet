using DAL.Data;
using Shared.Filters;
using Shared.Modifiers;
using System.Threading.Tasks;
using SQLite.Net.Async;
using System.Collections.Generic;

namespace DAL.DataAccess
{
    public class Categories : BaseModifiableCrudDataAccess<Category, CategoryFilter, CategoryModifier>, ICategories
    {
        private static readonly IImages images = new Images();
        private static readonly IWalletsCategories walletsCategories = new WalletsCategories();

        protected async override Task ApplyModifiersAsync(Category category, CategoryModifier modifier)
        {
            if ((modifier.IncludeImage || modifier.IncludeAll) && category.ImageId.HasValue)
                category.Image = await images.GetAsync(category.ImageId.Value);

            if (modifier.IncludeWallets || modifier.IncludeAll)
            {
                var walletCategoryModifier = new WalletCategoryModifier() { IncludeWallet = true };
                var filter = new WalletCategoryFilter() { CategoryId = category.Id };
                var wallCats = await walletsCategories.GetAsync(filter, walletCategoryModifier);
                category.Wallets = new List<Wallet>();

                foreach (var walletCategory in wallCats)
                {
                    if (walletCategory.Wallet != null)
                        category.Wallets.Add(walletCategory.Wallet);
                }
            }
                
        }

        protected override AsyncTableQuery<Category> ApplyFilters(AsyncTableQuery<Category> query, CategoryFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(x => x.Name.Contains(filter.Name));

            if (filter.ImageId.HasValue)
                query = query.Where(x => x.ImageId == filter.ImageId.Value);

            return query;
        }
    }
}
