using BL.Models;
using Shared.Filters;
using Shared.Modifiers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Service
{
    public interface ICategoryService : ICrud<Category, CategoryFilter>
    {
        Task<IList<Category>> GetAll(CategoryModifier modifier);

        Task<IList<Category>> Get(CategoryFilter filter, CategoryModifier modifier);

        Task<Category> Get(int id, CategoryModifier modifier);
    }
}
