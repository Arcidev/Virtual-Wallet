using DAL.Data;
using Shared.Filters;
using Shared.Modifiers;
using System.Threading.Tasks;
using SQLite.Net.Async;

namespace DAL.DataAccess
{
    public class Categories : BaseModifiableCrudDataAccess<Category, CategoryFilter, CategoryModifier>, ICategories
    {
        private static readonly IImages images = new Images();

        protected async override Task ApplyModifiers(Category category, CategoryModifier modifier)
        {
            if ((modifier.IncludeImage || modifier.IncludeAll) && category.ImageId.HasValue)
                category.Image = await images.Get(category.ImageId.Value);
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
