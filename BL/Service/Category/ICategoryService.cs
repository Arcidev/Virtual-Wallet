using BL.Models;
using Shared.Filters;
using Shared.Modifiers;

namespace BL.Service
{
    public interface ICategoryService : IModifiableCrudService<Category, CategoryFilter, CategoryModifier>
    {
    }
}
