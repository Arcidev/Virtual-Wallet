using BL.Models;
using Shared.Filters;
using System.Threading.Tasks;

namespace BL.Service
{
    public interface ICrudService<T1, T2> : IGetService<T1, T2> where T1 : IDto where T2 : BaseFilter
    {
        Task CreateAsync(params T1[] entities);

        Task UpdateAsync(params T1[] entities);

        Task ReplaceAsync(params T1[] entities);

        Task DeleteAsync(int id);

        Task DeleteAllAsync();
    }
}
