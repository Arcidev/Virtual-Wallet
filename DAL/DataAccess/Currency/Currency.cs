using DAL.Data;
using Shared.Filters;
using SQLite.Net.Async;

namespace DAL.DataAccess
{
    public class Currencies : BaseCrudDataAccess<Currency, CurrencyFilter>, ICurrencies
    {
        protected override AsyncTableQuery<Currency> ApplyFilters(AsyncTableQuery<Currency> query, CurrencyFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.Code))
                query = query.Where(x => x.Code.Contains(filter.Code));

            return base.ApplyFilters(query, filter);
        }
    }
}
