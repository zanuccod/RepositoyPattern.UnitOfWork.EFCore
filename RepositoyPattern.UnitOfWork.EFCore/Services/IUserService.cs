using RepositoyPattern.UnitOfWork.EFCore.Core.IRepositories;
using RepositoyPattern.UnitOfWork.EFCore.Dto;

namespace RepositoyPattern.UnitOfWork.EFCore.Services
{
    public interface IUserService : IGenericRepository<UserDto>
    {
    }
}
