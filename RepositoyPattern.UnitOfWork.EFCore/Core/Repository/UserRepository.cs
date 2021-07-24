using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RepositoyPattern.UnitOfWork.EFCore.Core.IRepositories;
using RepositoyPattern.UnitOfWork.EFCore.Data;
using RepositoyPattern.UnitOfWork.EFCore.Models;

namespace RepositoyPattern.UnitOfWork.EFCore.Core.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context, ILogger<UserRepository> logger)
            :base(context, logger)
        { }

        public override async Task<bool> UpdateAsync(User entity)
        {
            var existingUser = await dbSet
                .FirstOrDefaultAsync(x => x.Id.Equals(entity.Id));

            if (existingUser == null)
            {
                return await AddAsync(entity);
            }
            else
            {
                existingUser.FirstName = entity.FirstName;
                existingUser.LastName = entity.LastName;
                existingUser.Email = entity.Email;
                return true;
            }
        }

        public override async Task<bool> DeleteAsync(Guid id)
        {
            var exist = await dbSet
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (exist == null)
            {
                return false;
            }
            else
            {
                dbSet.Remove(exist);
                return true;
            }
        }
    }
}
