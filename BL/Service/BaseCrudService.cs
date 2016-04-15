using System.Threading.Tasks;
using Shared.Filters;
using BL.Models;
using BL.Mapping;

namespace BL.Service
{
    public class BaseCrudService<T1, T2, T3, T4> : BaseGetService<T1, T2, T3, T4> where T1 : IDto where T2 : DAL.Data.IDao where T3 : class, DAL.DataAccess.ICrud<T2, T4>, new() where T4 : BaseFilter
    {
        public async Task Create(params T1[] entities)
        {
            await _instance.Create(MapperInstance.Mapper.Map<T2[]>(entities));
        }

        public async Task Update(params T1[] entities)
        {
            await _instance.Update(MapperInstance.Mapper.Map<T2[]>(entities));
        }

        public async Task Delete(int id)
        {
            await _instance.Delete(id);
        }

        public async Task DeleteAll()
        {
            await _instance.DeleteAll();
        }
    }
}
