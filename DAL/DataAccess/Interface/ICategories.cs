using DAL.Data;
using Shared.Filters;
using Shared.Modifiers;

namespace DAL.DataAccess
{
    public interface ICategories : IModifiableCrud<Category, CategoryFilter, CategoryModifier>
    {
    }
}
