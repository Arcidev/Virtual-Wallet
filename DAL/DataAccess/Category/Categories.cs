using DAL.Data;
using Shared.Filters;
using Shared.Modifiers;
using System.Threading.Tasks;
using SQLite.Net.Async;
using System.Collections.Generic;
using System.Linq;

namespace DAL.DataAccess
{
    public class Categories : BaseModifiableCrudDataAccess<Category, CategoryFilter, CategoryModifier>, ICategories
    {
        private static readonly IImages images = new Images();
        private static readonly IWalletsCategories walletsCategories = new WalletsCategories();
        private static readonly ICategoriesRules categoriesRules = new CategoriesRules();

        protected async override Task ApplyModifiersAsync(Category category, CategoryModifier modifier)
        {
            if ((modifier.IncludeImage || modifier.IncludeAll) && category.ImageId.HasValue)
                category.Image = await images.GetAsync(category.ImageId.Value);

            if (modifier.IncludeWallets || modifier.IncludeAll)
            {
                var walletCategoryModifier = new WalletCategoryModifier() { IncludeWallet = true };
                var filter = new WalletCategoryFilter() { CategoryId = category.Id };
                var wallCats = await walletsCategories.GetAsync(filter, walletCategoryModifier);
                category.Wallets = wallCats.Where(x => x.Wallet != null).Select(x => x.Wallet).ToList();
            }
             
            if (modifier.IncludeRules || modifier.IncludeAll)
            {
                var categoryRuleModifier = new CategoryRuleModifier() { IncludeRule = true };
                var filter = new CategoryRuleFilter() { CategoryId = category.Id };
                var catsRules = await categoriesRules.GetAsync(filter, categoryRuleModifier);
                category.Rules = catsRules.Where(x => x.Rule != null).Select(x => x.Rule).ToList();
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

        protected override async Task OnEntityDeletedAsync(SQLiteAsyncConnection connection, int id)
        {
            await connection.ExecuteAsync($"DELETE FROM {nameof(WalletCategory)} WHERE {nameof(WalletCategory.CategoryId)} = {id}");
            await connection.ExecuteAsync($"DELETE FROM {nameof(CategoryRule)} WHERE {nameof(CategoryRule.CategoryId)} = {id}");
        }
    }
}
