using DAL.Data;
using Shared.Filters;

namespace DAL.DataAccess
{
    public interface ICurrencies : ICrud<Currency, CurrencyFilter>
    {
    }
}
