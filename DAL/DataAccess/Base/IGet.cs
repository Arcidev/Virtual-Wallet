using DAL.Data;
using Shared.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    /// <summary>
    /// Interface for get db operations upon entity
    /// </summary>
    /// <typeparam name="T1">Entity</typeparam>
    /// <typeparam name="T2">Filter</typeparam>
    public interface IGet<T1, T2> where T1 : IDao where T2 : BaseFilter
    {
        /// <summary>
        /// Gets all entities
        /// </summary>
        /// <returns>List of all entities</returns>
        Task<IList<T1>> GetAllAsync();

        /// <summary>
        /// Gets filtered entities
        /// </summary>
        /// <param name="filter">Filter to apply</param>
        /// <returns>List of filtered entities</returns>
        Task<IList<T1>> GetAsync(T2 filter = null);

        /// <summary>
        /// Gets entity by id
        /// </summary>
        /// <param name="id">Id of entity to get</param>
        /// <returns>Entity if found, otherwise null</returns>
        Task<T1> GetAsync(int id);
    }
}
