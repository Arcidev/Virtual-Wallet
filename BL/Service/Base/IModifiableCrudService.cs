using BL.Models;
using Shared.Filters;
using Shared.Modifiers;

namespace BL.Service
{
    public interface IModifiableCrudService<T1, T2, T3> : IModifiableGetService<T1, T2, T3>, ICrudService<T1, T2> where T1 : IDto where T2 : BaseFilter where T3 : BaseModifier
    {
    }
}
