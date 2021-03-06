﻿using BL.Models;
using Shared.Filters;
using Shared.Modifiers;

namespace BL.Service
{
    public interface IRuleService : IModifiableCrudService<Rule, RuleFilter, RuleModifier>
    {
    }
}
