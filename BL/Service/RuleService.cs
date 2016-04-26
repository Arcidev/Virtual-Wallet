using DAL.DataAccess;
using Shared.Filters;
using Shared.Modifiers;
using BL.Models;

namespace BL.Service
{
    public class RuleService : BaseModifiableCrudService<Rule, DAL.Data.Rule, Rules, RuleFilter, RuleModifier>, IRuleService
    {
    }
}
