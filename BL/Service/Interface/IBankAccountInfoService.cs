using BL.Models;
using Shared.Filters;

namespace BL.Service
{
    public interface IBankAccountInfoService : ICrudService<BankAccountInfo, BaseFilter>
    {
    }
}
