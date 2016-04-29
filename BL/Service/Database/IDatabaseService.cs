using Shared.Enums;
using System.Threading.Tasks;

namespace BL.Service
{
    public interface IDatabaseService
    {
        Task InitAsync();

        Task RemoveAllDataAsync();

        Task<CopyDatabaseResult> CopyToRoamingFolderAsync();

        Task<CopyDatabaseResult> RetrieveFromRoamingFolderAsync();
    }
}
