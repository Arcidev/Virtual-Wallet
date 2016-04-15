using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.DataAccess;
using Shared.Filters;
using Shared.Modifiers;
using BL.Mapping;
using BL.Models;

namespace BL.Service
{
    public class CategoryService : BaseCrudService<Category, DAL.Data.Category, Categories, CategoryFilter>, ICategoryService
    {
        public async Task<Category> Get(int id, CategoryModifier modifier)
        {
            return MapperInstance.Mapper.Map<Category>(await _instance.Get(id, modifier));
        }

        public async Task<IList<Category>> Get(CategoryFilter filter, CategoryModifier modifier)
        {
            return MapperInstance.Mapper.Map<IList<Category>>(await _instance.Get(filter, modifier));
        }

        public async Task<IList<Category>> GetAll(CategoryModifier modifier)
        {
            return MapperInstance.Mapper.Map<IList<Category>>(await _instance.GetAll(modifier));
        }
    }
}
