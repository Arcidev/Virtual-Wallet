using BL.Models;
using Shared.Filters;
using Shared.Modifiers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mapster;

namespace BL.Service
{
    public abstract class BaseModifiableGetService<T1, T2, T3, T4, T5> : BaseGetService<T1, T2, T3, T4>, IModifiableGetService<T1, T4, T5> where T1 : IDto where T2 : DAL.Data.IDao where T3 : class, DAL.DataAccess.IModifiableGet<T2, T4, T5>, new() where T4 : BaseFilter where T5 : BaseModifier
    {
        public async Task<T1> GetAsync(int id, T5 modifier)
        {
            return (await _instance.GetAsync(id, modifier)).Adapt<T1>();
        }

        public async Task<IList<T1>> GetAsync(T4 filter, T5 modifier)
        {
            return (await _instance.GetAsync(filter, modifier)).Adapt<IList<T1>>();
        }

        public async Task<IList<T1>> GetAllAsync(T5 modifier)
        {
            return (await _instance.GetAllAsync(modifier)).Adapt<IList<T1>>();
        }
    }

    public abstract class BaseModifiableCrudService<T1, T2, T3, T4, T5> : BaseCrudService<T1, T2, T3, T4>, IModifiableCrudService<T1, T4, T5> where T1 : IDto where T2 : DAL.Data.IDao where T3 : class, DAL.DataAccess.IModifiableCrud<T2, T4, T5>, new() where T4 : BaseFilter where T5 : BaseModifier
    {
        public async Task<T1> GetAsync(int id, T5 modifier)
        {
            return (await _instance.GetAsync(id, modifier)).Adapt<T1>();
        }

        public async Task<IList<T1>> GetAsync(T4 filter, T5 modifier)
        {
            return (await _instance.GetAsync(filter, modifier)).Adapt<IList<T1>>();
        }

        public async Task<IList<T1>> GetAllAsync(T5 modifier)
        {
            return (await _instance.GetAllAsync(modifier)).Adapt<IList<T1>>();
        }
    }
}
