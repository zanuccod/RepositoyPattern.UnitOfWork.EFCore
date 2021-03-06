using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RepositoyPattern.UnitOfWork.EFCore.Core.IRepositories;
using RepositoyPattern.UnitOfWork.EFCore.Data;
using RepositoyPattern.UnitOfWork.EFCore.Models;

namespace RepositoyPattern.UnitOfWork.EFCore.Core.Repository
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected ApplicationDbContext context;
        internal DbSet<T> dbSet;
        public readonly ILogger _logger;

        public GenericRepository(ApplicationDbContext context, ILogger<GenericRepository<T>> logger)
        {
            this.context = context;
            dbSet = context.Set<T>();
            _logger = logger;
        }

        public virtual async Task<T> GetByIdAsync(int id)
            => await dbSet.FindAsync(id);

        public virtual async Task<T> AddAsync(T entity)
        {
            var item = await dbSet.AddAsync(entity);
            return item.Entity;
        }

        public virtual async Task<IEnumerable<T>> FindAllAsync()
            => await dbSet.ToArrayAsync();

        public async virtual Task<bool> DeleteAsync(int id)
        {
            var exist = await GetByIdAsync(id);
            if (exist == null)
            {
                return false;
            }

            await Task.Run(() => dbSet.Remove(exist));
            return true;
        }

        public async virtual Task<T> UpdateAsync(T entity)
        {
            var tmp = await GetByIdAsync(entity.Id);
            if (tmp == null)
            {
                await AddAsync(entity);
            }
            else
            {
                tmp = entity;
            }
            return tmp;
        }
    }
}
