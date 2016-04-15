using DAL.Data;
using Shared.Filters;
using Shared.Modifiers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public interface IModifiableGet<T1, T2, T3> : IGet<T1, T2> where T1 : IDao where T2 : BaseFilter where T3 : BaseModifier
    {
        Task<IList<T1>> GetAll(T3 modifier = null);

        Task<IList<T1>> Get(T2 filter = null, T3 modifier = null);

        Task<T1> Get(int id, T3 modifier = null);
    }
}
