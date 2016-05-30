using BL.Models;
using Shared.Filters;
using Shared.Modifiers;

namespace BL.Service
{
    public interface ICurrencyService : IModifiableCrudService<Currency, CurrencyFilter, CurrencyModifier>
    {
    }
}
