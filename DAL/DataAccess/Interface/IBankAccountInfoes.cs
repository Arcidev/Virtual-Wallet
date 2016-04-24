using DAL.Data;
using Shared.Filters;

namespace DAL.DataAccess
{
    public interface IBankAccountInfoes : ICrud<BankAccountInfo, BaseFilter>
    {
    }
}
