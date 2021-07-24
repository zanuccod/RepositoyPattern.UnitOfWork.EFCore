using AutoMapper;
using RepositoyPattern.UnitOfWork.EFCore.Dto;
using RepositoyPattern.UnitOfWork.EFCore.Models;

namespace RepositoyPattern.UnitOfWork.EFCore.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
