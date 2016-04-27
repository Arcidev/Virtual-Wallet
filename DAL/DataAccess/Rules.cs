using DAL.Data;
using Shared.Filters;
using Shared.Modifiers;
using System.Threading.Tasks;
using SQLite.Net.Async;
using System.Collections.Generic;

namespace DAL.DataAccess
{
    public class Rules : BaseModifiableCrudDataAccess<Rule, RuleFilter, RuleModifier>, IRules
    {
        private static readonly IImages images = new Images();
        private static readonly ICategoriesRules categoriesRules = new CategoriesRules();

        protected async override Task ApplyModifiersAsync(Rule rule, RuleModifier modifier)
        {
            if (modifier.IncludeCategory || modifier.IncludeAll)
            {
                var categoryRuleModifier = new CategoryRuleModifier() { IncludeCategory = true };
                var filter = new CategoryRuleFilter() { RuleId = rule.Id };
                var catsRules = await categoriesRules.GetAsync(filter, categoryRuleModifier);
                rule.categories = new List<Category>();

                foreach (var categoryRule in catsRules)
                {
                    if (categoryRule.Category != null)
                        rule.categories.Add(categoryRule.Category);
                }
            }
                
        }

        protected override AsyncTableQuery<Rule> ApplyFilters(AsyncTableQuery<Rule> query, RuleFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(x => x.Name.Contains(filter.Name));

            return query;
        }

        protected override async Task OnEntityDeletedAsync(SQLiteAsyncConnection connection, int id)
        {
            await connection.ExecuteAsync($"DELETE FROM {nameof(CategoryRule)} WHERE {nameof(CategoryRule.RuleId)} = {id}");
        }
    }
}
