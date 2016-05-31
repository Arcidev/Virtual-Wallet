using BL.Models;
using Shared.Filters;

namespace BL.Service
{
    public interface ICurrencyService : ICrudService<Currency, CurrencyFilter>
    {
    }
}
