using DAL.Data;
using Shared.Filters;
using Shared.Modifiers;
using System.Threading.Tasks;
using SQLite.Net.Async;

namespace DAL.DataAccess
{
    public class Categories : BaseModifiableCrudDataAccess<Category, CategoryFilter, CategoryModifier>, ICategories
    {
        private static readonly IIcons icons = new Icons();

        protected async override Task ApplyModifiers(Category category, CategoryModifier modifier)
        {
            if (modifier.IncludeIcon && category.IconId.HasValue)
                category.Icon = await icons.Get(category.IconId.Value);
        }

        protected override AsyncTableQuery<Category> ApplyFilters(AsyncTableQuery<Category> query, CategoryFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(x => x.Name.Contains(filter.Name));

            if (filter.IconId.HasValue)
                query = query.Where(x => x.IconId == filter.IconId.Value);

            return query;
        }
    }
}
