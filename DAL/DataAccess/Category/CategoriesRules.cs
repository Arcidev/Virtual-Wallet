using DAL.Data;
using Shared.Filters;
using Shared.Modifiers;
using SQLite.Net.Async;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public class CategoriesRules : BaseModifiableCrudDataAccess<CategoryRule, CategoryRuleFilter, CategoryRuleModifier>, ICategoriesRules
    {
        private static readonly IRules rules = new Rules();
        private static readonly ICategories categories = new Categories();

        protected async override Task ApplyModifiersAsync(CategoryRule categoryRule, CategoryRuleModifier modifier)
        {
            if (modifier.IncludeRule || modifier.IncludeAll)
                categoryRule.Rule = await rules.GetAsync(categoryRule.RuleId);

            if (modifier.IncludeCategory || modifier.IncludeAll)
                categoryRule.Category = await categories.GetAsync(categoryRule.CategoryId, new CategoryModifier() { IncludeImage = true });

        }

        protected override AsyncTableQuery<CategoryRule> ApplyFilters(AsyncTableQuery<CategoryRule> query, CategoryRuleFilter filter)
        {
            if (filter.RuleId.HasValue && filter.CategoryId.HasValue)
                query = query.Where(x => x.RuleId.Equals(filter.RuleId) && x.CategoryId.Equals(filter.CategoryId));

            else if (filter.RuleId.HasValue)
                query = query.Where(x => x.RuleId.Equals(filter.RuleId));

            else if (filter.CategoryId.HasValue)
                query = query.Where(x => x.CategoryId.Equals(filter.CategoryId));

            return base.ApplyFilters(query, filter);
        }
    }
}
