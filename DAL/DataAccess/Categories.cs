using DAL.Data;
using DAL.Helpers;
using Shared.Filters;
using Shared.Modifiers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public class Categories : BaseDataAccess, ICategories
    {
        private static readonly IIcons icons = new Icons();

        public async Task<IList<Category>> GetAll()
        {
            return await GetAll(null);
        }

        public async Task<IList<Category>> GetAll(CategoryModifier modifier)
        {
            return await Get(null, modifier);
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

        public async Task<Category> Get(int id)
        {
            return await Get(id, null);
        }

        public async Task<Category> Get(int id, CategoryModifier modifier)
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            var category = await connection.Table<Category>().Where(x => x.Id == id).FirstOrDefaultAsync();
            if (modifier != null && category != null)
                await ApplyModifiers(category, modifier);

            return category;
        }

        public async Task Create(params Category[] categories)
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            await connection.InsertAllAsync(categories);
        }

        public async Task Update(params Category[] categories)
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            await connection.UpdateAllAsync(categories);
        }

        public async Task Delete(int id)
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            await connection.DeleteAsync(new Category() { Id = id });
        }

        public async Task DeleteAll()
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            await connection.DeleteAllAsync<Category>();
        }

        private async Task ApplyModifiers(Category category, CategoryModifier modifier)
        {
            if (modifier.IncludeIcon && category.IconId != 0)
                category.Icon = await icons.Get(category.IconId);
        }

        private async Task ApplyModifiers(IList<Category> categories, CategoryModifier modifier)
        {
            foreach (var category in categories)
                await ApplyModifiers(category, modifier);
        }
    }
}
