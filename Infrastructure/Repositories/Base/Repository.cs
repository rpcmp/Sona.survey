using Core.Entities.Base;
using Core.Repositories.Base;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Base
{
    public abstract class Repository<T> : IRepository<T>
        where T : Entity
    {
        protected readonly ApplicationDbContext _dbContext;

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> GetQuery()
        {
            var set = _dbContext.Set<T>();
            return set.AsQueryable();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var set = _dbContext.Set<T>();
            return await set.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var set = _dbContext.Set<T>();
            return await set.ToArrayAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            var set = _dbContext.Set<T>();
            var result = await set.AddAsync(entity);
            return result.Entity;
        }

        public Task<T> UpdateAsync(T entity)
        {
            var set = _dbContext.Set<T>();
            var result = set.Update(entity);
            return Task.FromResult(result.Entity);
        }

        public Task DeleteAsync(T entity)
        {
            var set = _dbContext.Set<T>();
            set.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var set = _dbContext.Set<T>();
            var entity = await set.FindAsync(id);
            set.Remove(entity);
        }

        public Task Commit()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}
