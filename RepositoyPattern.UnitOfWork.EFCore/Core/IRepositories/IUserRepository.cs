using System;
using RepositoyPattern.UnitOfWork.EFCore.Models;

namespace RepositoyPattern.UnitOfWork.EFCore.Core.IRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
    }
}
