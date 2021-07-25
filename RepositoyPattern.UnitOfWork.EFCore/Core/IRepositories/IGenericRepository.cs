using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoyPattern.UnitOfWork.EFCore.Core.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> FindAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task<bool> DeleteAsync(int id);
        Task<T> UpdateAsync(T entity);
    }
}
