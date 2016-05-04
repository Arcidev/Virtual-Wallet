using BL.Models;
using Shared.Filters;
using System.Threading.Tasks;

namespace BL.Service
{
    /// <summary>
    /// Interface for CRUD service operations upon entity
    /// </summary>
    /// <typeparam name="T1">Entity</typeparam>
    /// <typeparam name="T2">Filter</typeparam>
    public interface ICrudService<T1, T2> : IGetService<T1, T2> where T1 : IDto where T2 : BaseFilter
    {
        /// <summary>
        /// Inserts entities into database
        /// </summary>
        /// <param name="storeDbId">Determines if id should be stored back in dto after insert</param>
        /// <param name="entities">Entities to be stored</param>
        Task InsertAsync(bool storeDbId, params T1[] entities);

        /// <summary>
        /// Inserts or ignores entities into database
        /// </summary>
        /// <param name="storeDbId">Determines if id should be stored back in dto after insert</param>
        /// <param name="entities">Entities to be stored or ignored</param>
        Task InsertOrIgnoreAsync(bool storeDbId, params T1[] entities);

        /// <summary>
        /// Inserts or replaces entities into database
        /// </summary>
        /// <param name="storeDbId">Determines if id should be stored back in dto after insert</param>
        /// <param name="entities">Entities to be stored or replaced</param>
        Task InsertOrReplaceAsync(bool storeDbId, params T1[] entities);

        /// <summary>
        /// Updates existing entities
        /// </summary>
        /// <param name="entities">Entities to be updated</param>
        Task UpdateAsync(params T1[] entities);

        /// <summary>
        /// Delets existing entities
        /// </summary>
        /// <param name="id">Id of entity to be deleted</param>
        Task DeleteAsync(int id);

        /// <summary>
        /// Deletes all stored entities
        /// </summary>
        Task DeleteAllAsync();
    }
}
