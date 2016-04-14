using DAL.Data;
using Shared.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public interface ICategories
    {
        Task<IList<Category>> GetAll();

        Task<IList<Category>> Get(CategoryFilter filter);

        Task<Category> Get(int id);

        Task Create(params Category[] categories);

        Task Update(params Category[] categories);

        Task Delete(int id);

        Task DeleteAll();
    }
}
