using BL.Models;
using Shared.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mapster;

namespace BL.Service
{
    public class BaseGetService<T1, T2, T3, T4> : IGetService<T1, T4> where T1 : IDto where T2 : DAL.Data.IDao where T3 : class, DAL.DataAccess.IGet<T2, T4>, new() where T4 : BaseFilter
    {
        protected static readonly T3 _instance = new T3();

        public async Task<T1> GetAsync(int id)
        {
            return (await _instance.GetAsync(id)).Adapt<T1>();
        }

        public async Task<IList<T1>> GetAsync(T4 filter = null)
        {
            return (await _instance.GetAsync(filter)).Adapt<IList<T1>>();
        }

        public async Task<IList<T1>> GetAllAsync()
        {
            return (await _instance.GetAllAsync()).Adapt<IList<T1>>();
        }
    }
}
