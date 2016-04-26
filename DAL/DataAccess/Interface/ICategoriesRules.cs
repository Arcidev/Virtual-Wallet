using DAL.Data;
using Shared.Filters;
using Shared.Modifiers;

namespace DAL.DataAccess
{
    public interface ICategoriesRules : IModifiableGet<CategoryRule, CategoryRuleFilter, CategoryRuleModifier>
    {
    }
}
