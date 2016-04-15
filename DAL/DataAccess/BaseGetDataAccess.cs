﻿using DAL.Data;
using DAL.Helpers;
using Shared.Filters;
using SQLite.Net.Async;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public abstract class BaseGetDataAccess<T1, T2> where T1 : class, IDao, new() where T2 : BaseFilter, new()
    {
        protected AsyncTableQuery<T1> ApplyBaseFilters(AsyncTableQuery<T1> query, BaseFilter filter)
        {
            if (filter.Ids != null && filter.Ids.Any())
                query = query.Where(x => filter.Ids.Contains(x.Id));

            if (filter.Skip > 0)
                query = query.Skip(filter.Skip.Value);

            if (filter.Take > 0)
                query = query.Take(filter.Take.Value);

            return query;
        }

        public async Task<IList<T1>> Get(T2 filter)
        {
            if (filter == null)
                filter = new T2();

            var connection = ConnectionHelper.GetDbAsyncConnection();
            return await ApplyFilters(connection.Table<T1>(), filter).ToListAsync();
        }

        public async Task<IList<T1>> GetAll()
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            return await connection.Table<T1>().ToListAsync();
        }

        public async Task<T1> Get(int id)
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            return await connection.Table<T1>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        protected abstract AsyncTableQuery<T1> ApplyFilters(AsyncTableQuery<T1> query, T2 filter);
    }
}