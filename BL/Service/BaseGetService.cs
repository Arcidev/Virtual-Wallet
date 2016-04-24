using BL.Models;
using BL.Mapping;
using Shared.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Service
{
    public class BaseGetService<T1, T2, T3, T4> : IGetService<T1, T4> where T1 : IDto where T2 : DAL.Data.IDao where T3 : class, DAL.DataAccess.IGet<T2, T4>, new() where T4 : BaseFilter
    {
        protected static readonly T3 _instance = new T3();

        public async Task<T1> GetAsync(int id)
        {
            return MapperInstance.Mapper.Map<T1>(await _instance.GetAsync(id));
        }

        public async Task<IList<T1>> GetAsync(T4 filter = null)
        {
            return MapperInstance.Mapper.Map<IList<T1>>(await _instance.GetAsync(filter));
        }

        public async Task<IList<T1>> GetAllAsync()
        {
            return MapperInstance.Mapper.Map<IList<T1>>(await _instance.GetAllAsync());
        }
    }
}
