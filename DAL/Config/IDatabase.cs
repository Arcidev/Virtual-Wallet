using Shared.Enums;
using System.Threading.Tasks;

namespace DAL.Config
{
    /// <summary>
    /// Interface for direct db operations
    /// </summary>
    public interface IDatabase
    {
        /// <summary>
        /// Initializes database storage
        /// </summary>
        Task InitAsync();

        /// <summary>
        /// Removes all user data stored in database
        /// </summary>
        Task RemoveAllDataAsync();

        /// <summary>
        /// Copies database into roaming folder
        /// </summary>
        Task<CopyDatabaseResult> CopyToRoamingFolder();

        /// <summary>
        /// Retrieves database from roaming folder
        /// </summary>
        Task<CopyDatabaseResult> RetrieveFromRoamingFolder();
    }
}
