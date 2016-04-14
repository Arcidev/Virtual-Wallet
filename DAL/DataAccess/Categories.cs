using DAL.Data;
using DAL.Helpers;
using Shared.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace DAL.DataAccess
{
    public class Categories : BaseDataAccess, ICategories
    {
        public async Task<IList<Category>> GetAll()
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            return await connection.Table<Category>().ToListAsync();
        }

        public async Task<IList<Category>> Get(CategoryFilter filter)
        {
            if (filter == null)
                filter = new CategoryFilter();

            var connection = ConnectionHelper.GetDbAsyncConnection();
            var query = connection.Table<Category>();

            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(x => x.Name.Contains(filter.Name));

            if (filter.IconId.HasValue)
                query = query.Where(x => x.IconId == filter.IconId.Value);

            return await ApplyBaseFilters(query, filter).ToListAsync();
        }

        public async Task<Category> Get(int id)
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            return await connection.Table<Category>().Where(x => x.Id == id).FirstAsync();
        }

        public async Task Create(params Category[] categories)
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            await connection.InsertAllAsync(categories);
        }

        public async Task Update(params Category[] categories)
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            await connection.UpdateAllAsync(categories);
        }

        public async Task Delete(int id)
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            await connection.DeleteAsync(new Category() { Id = id });
        }

        public async Task DeleteAll()
        {
            var connection = ConnectionHelper.GetDbAsyncConnection();
            await connection.DeleteAllAsync(typeof(Category));
        }
    }
}
