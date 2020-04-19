﻿using DAL.Data;
using Shared.Filters;
using Shared.Modifiers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public abstract class BaseModifiableGetDataAccess<T1, T2, T3> : BaseGetDataAccess<T1, T2>, IModifiableGet<T1, T2, T3> where T1 : class, IDao, new() where T2 : BaseFilter, new() where T3 : BaseModifier
    {
        public async Task<List<T1>> GetAllAsync(T3 modifier)
        {
            var entities = await GetAllAsync();
            if (modifier != null)
                await ApplyModifiersAsync(entities, modifier);

            return entities;
        }

        public async Task<List<T1>> GetAsync(T2 filter, T3 modifier)
        {
            var entities = await GetAsync(filter);
            if (modifier != null)
                await ApplyModifiersAsync(entities, modifier);

            return entities;
        }

        public async Task<T1> GetAsync(int id, T3 modifier)
        {
            var entity = await GetAsync(id);
            if (modifier != null && entity != null)
                await ApplyModifiersAsync(entity, modifier);

            return entity;
        }

        protected abstract Task ApplyModifiersAsync(T1 entity, T3 modifier);

        protected async Task ApplyModifiersAsync(IEnumerable<T1> entities, T3 modifier)
        {
            foreach (var entity in entities)
                await ApplyModifiersAsync(entity, modifier);
        }
    }

    public abstract class BaseModifiableCrudDataAccess<T1, T2, T3> : BaseCrudDataAccess<T1, T2>, IModifiableCrud<T1, T2, T3> where T1 : class, IDao, new() where T2 : BaseFilter, new() where T3 : BaseModifier
    {
        public async Task<List<T1>> GetAllAsync(T3 modifier)
        {
            var entities = await GetAllAsync();
            if (modifier != null)
                await ApplyModifiersAsync(entities, modifier);

            return entities;
        }

        public async Task<List<T1>> GetAsync(T2 filter, T3 modifier)
        {
            var entities = await GetAsync(filter);
            if (modifier != null)
                await ApplyModifiersAsync(entities, modifier);

            return entities;
        }

        public async Task<T1> GetAsync(int id, T3 modifier)
        {
            var entity = await GetAsync(id);
            if (modifier != null && entity != null)
                await ApplyModifiersAsync(entity, modifier);

            return entity;
        }

        protected abstract Task ApplyModifiersAsync(T1 entity, T3 modifier);

        protected async Task ApplyModifiersAsync(IEnumerable<T1> entities, T3 modifier)
        {
            foreach (var entity in entities)
                await ApplyModifiersAsync(entity, modifier);
        }
    }

}
