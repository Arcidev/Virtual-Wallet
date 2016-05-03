﻿using BL.Models;
using DAL.DataAccess;
using Shared.Filters;
using Shared.Modifiers;

namespace BL.Service
{
    public class CategoryRuleService : BaseModifiableCrudService<CategoryRule, DAL.Data.CategoryRule, CategoriesRules, CategoryRuleFilter, CategoryRuleModifier>, ICategoryRuleService
    {
    }
}