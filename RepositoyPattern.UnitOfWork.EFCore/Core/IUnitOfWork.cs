using System;
using System.Threading.Tasks;
using RepositoyPattern.UnitOfWork.EFCore.Core.IRepositories;

namespace RepositoyPattern.UnitOfWork.EFCore.Core
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        Task CompleteAsync();
    }
}
