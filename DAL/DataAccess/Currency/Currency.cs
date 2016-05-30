using DAL.Data;
using Shared.Filters;
using Shared.Modifiers;
using System.Threading.Tasks;
using SQLite.Net.Async;

namespace DAL.DataAccess
{
    public class Currencies : BaseModifiableCrudDataAccess<Currency, CurrencyFilter, CurrencyModifier>, ICurrencies
    {

        protected async override Task ApplyModifiersAsync(Currency Currency, CurrencyModifier modifier)
        {
            return;
        }

        protected override AsyncTableQuery<Currency> ApplyFilters(AsyncTableQuery<Currency> query, CurrencyFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.Code))
                query = query.Where(x => x.Code.Contains(filter.Code));

            return base.ApplyFilters(query, filter);
        }
    }
}
