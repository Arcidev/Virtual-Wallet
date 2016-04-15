using DAL.Data;
using DAL.Helpers;
using Shared.Filters;
using Shared.Modifiers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public class Categories : BaseDataAccess<Category>, ICategories
    {
        private static readonly IIcons icons = new Icons();

        public async Task<IList<Category>> GetAll(CategoryModifier modifier)
        {
            var categories = await GetAll();
            if (modifier != null)
                await ApplyModifiers(categories, modifier);

            return categories;
        }

        public async Task<IList<Category>> Get(CategoryFilter filter = null)
        {
            return await Get(filter, null);
        }

        public async Task<IList<Category>> Get(CategoryFilter filter, CategoryModifier modifier)
        {
            if (filter == null)
                filter = new CategoryFilter();

            var connection = ConnectionHelper.GetDbAsyncConnection();
            var query = connection.Table<Category>();

            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(x => x.Name.Contains(filter.Name));

            if (filter.IconId.HasValue)
                query = query.Where(x => x.IconId == filter.IconId.Value);

            var categories = await ApplyBaseFilters(query, filter).ToListAsync();
            if (modifier != null)
                await ApplyModifiers(categories, modifier);

            return categories;
        }

        public async Task<Category> Get(int id, CategoryModifier modifier)
        {
            var category = await Get(id);
            if (modifier != null && category != null)
                await ApplyModifiers(category, modifier);

            return category;
        }

        private async Task ApplyModifiers(Category category, CategoryModifier modifier)
        {
            if (modifier.IncludeIcon && category.IconId.HasValue)
                category.Icon = await icons.Get(category.IconId.Value);
        }

        private async Task ApplyModifiers(IList<Category> categories, CategoryModifier modifier)
        {
            foreach (var category in categories)
                await ApplyModifiers(category, modifier);
        }
    }
}
