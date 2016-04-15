using BL.Models;
using Shared.Filters;
using System.Threading.Tasks;

namespace BL.Service
{
    public interface ICrudService<T1, T2> : IGetService<T1, T2> where T1 : IDto where T2 : BaseFilter
    {
        Task Create(params T1[] entities);

        Task Update(params T1[] entities);

        Task Delete(int id);

        Task DeleteAll();
    }
}
