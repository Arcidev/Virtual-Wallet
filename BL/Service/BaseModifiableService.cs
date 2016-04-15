using BL.Models;
using BL.Mapping;
using Shared.Filters;
using Shared.Modifiers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Service
{
    public abstract class BaseModifiableGetService<T1, T2, T3, T4, T5> : BaseGetService<T1, T2, T3, T4> where T1 : IDto where T2 : DAL.Data.IDao where T3 : class, DAL.DataAccess.IModifiableGet<T2, T4, T5>, new() where T4 : BaseFilter where T5 : BaseModifier
    {
        public async Task<T1> Get(int id, T5 modifier)
        {
            return MapperInstance.Mapper.Map<T1>(await _instance.Get(id, modifier));
        }

        public async Task<IList<T1>> Get(T4 filter, T5 modifier)
        {
            return MapperInstance.Mapper.Map<IList<T1>>(await _instance.Get(filter, modifier));
        }

        public async Task<IList<T1>> GetAll(T5 modifier)
        {
            return MapperInstance.Mapper.Map<IList<T1>>(await _instance.GetAll(modifier));
        }
    }

    public abstract class BaseModifiableCrudService<T1, T2, T3, T4, T5> : BaseCrudService<T1, T2, T3, T4> where T1 : IDto where T2 : DAL.Data.IDao where T3 : class, DAL.DataAccess.IModifiableCrud<T2, T4, T5>, new() where T4 : BaseFilter where T5 : BaseModifier
    {
        public async Task<T1> Get(int id, T5 modifier)
        {
            return MapperInstance.Mapper.Map<T1>(await _instance.Get(id, modifier));
        }

        public async Task<IList<T1>> Get(T4 filter, T5 modifier)
        {
            return MapperInstance.Mapper.Map<IList<T1>>(await _instance.Get(filter, modifier));
        }

        public async Task<IList<T1>> GetAll(T5 modifier)
        {
            return MapperInstance.Mapper.Map<IList<T1>>(await _instance.GetAll(modifier));
        }
    }

}
