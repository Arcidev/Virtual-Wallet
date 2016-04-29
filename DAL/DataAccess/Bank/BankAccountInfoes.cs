using DAL.Data;
using Shared.Filters;

namespace DAL.DataAccess
{
    public class BankAccountInfoes : BaseCrudDataAccess<BankAccountInfo, BaseFilter>, IBankAccountInfoes
    {
    }
}
