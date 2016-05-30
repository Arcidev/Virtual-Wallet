using DAL.Data;
using Shared.Filters;
using Shared.Modifiers;

namespace DAL.DataAccess
{
    public interface ICurrencies : IModifiableCrud<Currency, CurrencyFilter, CurrencyModifier>
    {
    }
}
