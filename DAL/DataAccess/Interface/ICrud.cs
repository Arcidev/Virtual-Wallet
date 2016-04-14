using DAL.Data;
using Shared.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public interface ICrud<T1, T2> where T1 : IData where T2 : BaseFilter
    {
        Task<IList<T1>> GetAll();

        Task<IList<T1>> Get(T2 filter = null);

        Task<T1> Get(int id);

        Task Create(params T1[] entities);

        Task Update(params T1[] entities);

        Task Delete(int id);

        Task DeleteAll();
    }
}
