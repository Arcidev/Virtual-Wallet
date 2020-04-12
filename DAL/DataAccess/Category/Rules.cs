using DAL.Data;
using Shared.Filters;
using Shared.Modifiers;
using System.Threading.Tasks;
using SQLite;

namespace DAL.DataAccess
{
    public class Rules : BaseModifiableCrudDataAccess<Rule, RuleFilter, RuleModifier>, IRules
    {
        private static readonly IImages images = new Images();
        private static readonly ICategories categories = new Categories();

        protected async override Task ApplyModifiersAsync(Rule rule, RuleModifier modifier)
        {
            if (modifier.IncludeCategory || modifier.IncludeAll)
                rule.Category = await categories.GetAsync(rule.CategoryId);
        }

        protected override AsyncTableQuery<Rule> ApplyFilters(AsyncTableQuery<Rule> query, RuleFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(x => x.Name.Contains(filter.Name));

            if (!string.IsNullOrEmpty(filter.Pattern))
                query = query.Where(x => x.Pattern.Contains(filter.Pattern));

            if (filter.CategoryId.HasValue)
                query = query.Where(x => x.CategoryId == filter.CategoryId.Value);

            return base.ApplyFilters(query, filter);
        }
    }
}
