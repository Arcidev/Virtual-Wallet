using BL.Models;
using Shared.Filters;
using Shared.Modifiers;

namespace BL.Service
{
    /// <summary>
    /// Interface for modifiable CRUD service operations oppon entity
    /// </summary>
    /// <typeparam name="T1">Entity</typeparam>
    /// <typeparam name="T2">Filter</typeparam>
    /// <typeparam name="T3">Modifier</typeparam>
    public interface IModifiableCrudService<T1, T2, T3> : IModifiableGetService<T1, T2, T3>, ICrudService<T1, T2> where T1 : IDto where T2 : BaseFilter where T3 : BaseModifier
    {
    }
}
