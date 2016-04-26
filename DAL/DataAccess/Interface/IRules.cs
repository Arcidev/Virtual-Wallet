using DAL.Data;
using Shared.Filters;
using Shared.Modifiers;

namespace DAL.DataAccess
{
    public interface IRules : IModifiableCrud<Rule, RuleFilter, RuleModifier>
    {
    }
}
