using DAL.Data;
using Shared.Filters;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    /// <summary>
    /// Interface for CRUD db operations oppon entity
    /// </summary>
    /// <typeparam name="T1">Entity</typeparam>
    /// <typeparam name="T2">Filter</typeparam>
    public interface ICrud<T1, T2> : IGet<T1, T2> where T1 : IDao where T2 : BaseFilter
    {
        /// <summary>
        /// Inserts entities into database
        /// </summary>
        /// <param name="entities">Entities to store</param>
        Task InsertAsync(params T1[] entities);

        /// <summary>
        /// Inserts or ignores entities in database
        /// </summary>
        /// <param name="entities">Entities to be stored or replaced</param>
        Task InsertOrIgnoreAsync(params T1[] entities);

        /// <summary>
        /// Updates entities stored in db
        /// </summary>
        /// <param name="entities">Entities to update</param>
        Task UpdateAsync(params T1[] entities);

        /// <summary>
        /// Inserts or replaces entities stored in database
        /// </summary>
        /// <param name="entities">Entities to be stored or replaced</param>
        Task InsertOrReplaceAsync(params T1[] entities);

        /// <summary>
        /// Deletes entity by id
        /// </summary>
        /// <param name="id">Id of entity to be deleted</param>
        Task DeleteAsync(int id);

        /// <summary>
        /// Deletes all entities stored in database
        /// </summary>
        Task DeleteAllAsync();
    }
}
