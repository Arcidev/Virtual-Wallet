using DAL.Data;
using Shared.Filters;
using SQLite.Net.Async;

namespace DAL.DataAccess
{
    public class BankAccountInfoes : BaseCrudDataAccess<BankAccountInfo, BaseFilter>, IBankAccountInfoes
    {
        protected override AsyncTableQuery<BankAccountInfo> ApplyFilters(AsyncTableQuery<BankAccountInfo> query, BaseFilter filter)
        {
            return query;
        }
    }
}
