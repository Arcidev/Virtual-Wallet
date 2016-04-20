using System.Threading.Tasks;

namespace DAL.Config
{
    public interface IDatabase
    {
        Task InitAsync();

        Task RemoveAllDataAsync();
    }
}
