using DAL.Data;
using Shared.Filters;
using Shared.Modifiers;

namespace DAL.DataAccess
{
    /// <summary>
    /// Interface for modifiable CRUD db operations oppon entity
    /// </summary>
    /// <typeparam name="T1">Entity</typeparam>
    /// <typeparam name="T2">Filter</typeparam>
    /// <typeparam name="T3">Modifier</typeparam>
    public interface IModifiableCrud<T1, T2, T3> : IModifiableGet<T1, T2, T3>, ICrud<T1, T2> where T1 : IDao where T2 : BaseFilter where T3 : BaseModifier
    {
    }
}
