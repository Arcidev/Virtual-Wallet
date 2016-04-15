using BL.Models;
using Shared.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Service
{
    public interface IGetService<T1, T2> where T1 : IDto where T2 : BaseFilter
    {
        Task<IList<T1>> GetAll();

        Task<IList<T1>> Get(T2 filter = null);

        Task<T1> Get(int id);
    }
}
