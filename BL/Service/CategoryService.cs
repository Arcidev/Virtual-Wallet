using DAL.DataAccess;
using Shared.Filters;
using Shared.Modifiers;
using BL.Models;

namespace BL.Service
{
    public class CategoryService : BaseModifiableCrudService<Category, DAL.Data.Category, Categories, CategoryFilter, CategoryModifier>, ICategoryService
    {
    }
}
