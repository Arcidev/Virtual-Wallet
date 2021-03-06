﻿using BL.Models;
using Shared.Filters;
using Shared.Modifiers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Service
{
    /// <summary>
    /// Interface for modifiable get service operations oppon entity
    /// </summary>
    /// <typeparam name="T1">Entity</typeparam>
    /// <typeparam name="T2">Filter</typeparam>
    /// <typeparam name="T3">Modifier</typeparam>
    public interface IModifiableGetService<T1, T2, T3> : IGetService<T1, T2> where T1 : IDto where T2 : BaseFilter where T3 : BaseModifier
    {
        /// <summary>
        /// Gets all entities with referenced entities
        /// </summary>
        /// <param name="modifier">Specifies which referenced entities should be included</param>
        /// <returns>List of all entities with referenced entities based on modifier</returns>
        Task<List<T1>> GetAllAsync(T3 modifier = null);

        /// <summary>
        /// Gets filtered entities with referenced entities
        /// </summary>
        /// <param name="filter">Filter to apply</param>
        /// <param name="modifier">Specifies which referenced entities should be included</param>
        /// <returns>List  of filtered entities with referenced entities based on modifier</returns>
        Task<List<T1>> GetAsync(T2 filter = null, T3 modifier = null);

        /// <summary>
        /// Gets entity by id with referenced entities
        /// </summary>
        /// <param name="id">Id of entity to get</param>
        /// <param name="modifier">Specifies which referenced entities should be included</param>
        /// <returns>Entity with referenced entities based on modifier if found, otherwise null</returns>
        Task<T1> GetAsync(int id, T3 modifier = null);
    }
}
