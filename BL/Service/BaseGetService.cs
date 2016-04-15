using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Filters;
using BL.Models;
using BL.Mapping;

namespace BL.Service
{
    public class BaseGetService<T1, T2, T3, T4> where T1 : IDto where T2 : DAL.Data.IDao where T3 : class, DAL.DataAccess.IGet<T2, T4>, new() where T4 : BaseFilter
    {
        protected static readonly T3 _instance = new T3();

        public async Task<T1> Get(int id)
        {
            return MapperInstance.Mapper.Map<T1>(await _instance.Get(id));
        }

        public async Task<IList<T1>> Get(T4 filter = null)
        {
            return MapperInstance.Mapper.Map<IList<T1>>(await _instance.Get(filter));
        }

        public async Task<IList<T1>> GetAll()
        {
            return MapperInstance.Mapper.Map<IList<T1>>(await _instance.GetAll());
        }
    }
}
