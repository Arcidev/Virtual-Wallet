using DAL.DataAccess;
using Shared.Filters;
using Shared.Modifiers;
using BL.Models;

namespace BL.Service
{
    public class CurrencyService : BaseModifiableCrudService<Currency, DAL.Data.Currency, Currencies, CurrencyFilter, CurrencyModifier>, ICurrencyService
    {
    }
}
