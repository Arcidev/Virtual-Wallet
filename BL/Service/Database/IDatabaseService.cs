using Shared.Enums;
using System.Threading.Tasks;

namespace BL.Service
{
    /// <summary>
    /// Interface for direct db service operations
    /// </summary>
    public interface IDatabaseService
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
        Task<CopyDatabaseResult> CopyToRoamingFolderAsync();

        /// <summary>
        /// Retrieves database from roaming folder
        /// </summary>
        Task<CopyDatabaseResult> RetrieveFromRoamingFolderAsync();
    }
}
