using DAL.DataAccess;
using Shared.Filters;
using BL.Models;

namespace BL.Service
{
    public class CurrencyService : BaseCrudService<Currency, DAL.Data.Currency, Currencies, CurrencyFilter>, ICurrencyService
    {
    }
}
