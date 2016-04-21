using Shared.Enums;
using System.Threading.Tasks;

namespace BL.Service
{
    public interface IDatabaseService
    {
        Task InitAsync();

        Task RemoveAllDataAsync();

        Task<CopyDatabaseResult> CopyToRoamingFolder();

        Task<CopyDatabaseResult> RetrieveFromRoamingFolder();
    }
}
