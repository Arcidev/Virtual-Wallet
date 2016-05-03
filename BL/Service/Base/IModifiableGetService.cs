﻿using BL.Models;
using Shared.Filters;
using Shared.Modifiers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Service
{
    public interface IModifiableGetService<T1, T2, T3> : IGetService<T1, T2> where T1 : IDto where T2 : BaseFilter where T3 : BaseModifier
    {
        Task<IList<T1>> GetAllAsync(T3 modifier = null);

        Task<IList<T1>> GetAsync(T2 filter = null, T3 modifier = null);

        Task<T1> GetAsync(int id, T3 modifier = null);
    }
}