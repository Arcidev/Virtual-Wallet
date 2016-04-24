using DAL.Data;
using Shared.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public interface IGet<T1, T2> where T1 : IDao where T2 : BaseFilter
    {
        Task<IList<T1>> GetAllAsync();

        Task<IList<T1>> GetAsync(T2 filter = null);

        Task<T1> GetAsync(int id);
    }
}
