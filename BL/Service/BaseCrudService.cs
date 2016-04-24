using BL.Models;
using BL.Mapping;
using Shared.Filters;
using System.Threading.Tasks;

namespace BL.Service
{
    public class BaseCrudService<T1, T2, T3, T4> : BaseGetService<T1, T2, T3, T4>, ICrudService<T1, T4> where T1 : IDto where T2 : DAL.Data.IDao where T3 : class, DAL.DataAccess.ICrud<T2, T4>, new() where T4 : BaseFilter
    {
        public async Task CreateAsync(params T1[] entities)
        {
            await _instance.CreateAsync(MapperInstance.Mapper.Map<T2[]>(entities));
        }

        public async Task UpdateAsync(params T1[] entities)
        {
            await _instance.UpdateAsync(MapperInstance.Mapper.Map<T2[]>(entities));
        }

        public async Task ReplaceAsync(params T1[] entities)
        {
            await _instance.ReplaceAsync(MapperInstance.Mapper.Map<T2[]>(entities));
        }

        public async Task DeleteAsync(int id)
        {
            await _instance.DeleteAsync(id);
        }

        public async Task DeleteAllAsync()
        {
            await _instance.DeleteAllAsync();
        }
    }
}
