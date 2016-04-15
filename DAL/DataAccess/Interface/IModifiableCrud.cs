using DAL.Data;
using Shared.Filters;
using Shared.Modifiers;

namespace DAL.DataAccess
{
    public interface IModifiableCrud<T1, T2, T3> : IModifiableGet<T1, T2, T3>, ICrud<T1, T2> where T1 : IDao where T2 : BaseFilter where T3 : BaseModifier
    {
    }
}
