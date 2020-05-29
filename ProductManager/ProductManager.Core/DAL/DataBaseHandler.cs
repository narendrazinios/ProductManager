using Microsoft.EntityFrameworkCore;
using ProductManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProductManager.Core.DAL
{
    public interface IDataBaseHandler
    {
        Task<bool> AddAsync<T>(T entity) where T : GuidObject;
        Task<bool> UpdateAsync<T>(T entity) where T : GuidObject;
        Task<IEnumerable<T>> GetAsync<T>(Expression<Func<T, bool>> expression) where T : GuidObject;
        Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> expression) where T : GuidObject;
        Task<IEnumerable<T>> GetAllAsync<T>() where T : GuidObject;
        Task<bool> DeleteRangeAsync<T>(Expression<Func<T, bool>> expression) where T : GuidObject;
        Task<bool> DeleteAsync<T>(T entity) where T : GuidObject;

    }
    public class DataBaseHandler : IDataBaseHandler
    {
        private readonly DataContext _dataContext;
        public DataBaseHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<bool> AddAsync<T>(T entity) where T : GuidObject
        {
            _dataContext.Add(entity);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync<T>(T entity) where T : GuidObject
        {
            var activity = await _dataContext.Set<T>().Where(x => x.Id == entity.Id).FirstOrDefaultAsync();
            if (activity == null)
                throw new Exception("Could not find entity.");
            _dataContext.Remove(entity);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteRangeAsync<T>(Expression<Func<T, bool>> expression) where T : GuidObject
        {
            var dbSet = _dataContext.Set<T>();
            var activities = await dbSet.Where(expression).ToListAsync();
            if (activities == null)
                throw new Exception("Could not find entities.");
            dbSet.RemoveRange(activities);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<T>> GetAsync<T>(Expression<Func<T, bool>> expression) where T : GuidObject
        {
            return await _dataContext.Set<T>().Where(expression).ToListAsync();
        }
        public async Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> expression) where T : GuidObject
        {
            return await _dataContext.Set<T>().FirstOrDefaultAsync(expression);
        }
        public async Task<IEnumerable<T>> GetAllAsync<T>() where T : GuidObject
        {
            return await _dataContext.Set<T>().ToListAsync();
        }

        public async Task<bool> UpdateAsync<T>(T entity) where T : GuidObject
        {

            _dataContext.Entry(entity).State = EntityState.Modified;
            return await _dataContext.SaveChangesAsync() > 0;
        }
    }
}
