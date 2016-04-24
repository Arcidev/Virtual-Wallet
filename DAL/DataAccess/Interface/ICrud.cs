using DAL.Data;
using Shared.Filters;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public interface ICrud<T1, T2> : IGet<T1, T2> where T1 : IDao where T2 : BaseFilter
    {
        Task CreateAsync(params T1[] entities);

        Task UpdateAsync(params T1[] entities);

        Task ReplaceAsync(params T1[] entities);

        Task DeleteAsync(int id);

        Task DeleteAllAsync();
    }
}
