using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using RepositoyPattern.UnitOfWork.EFCore.Core;
using RepositoyPattern.UnitOfWork.EFCore.Dto;
using RepositoyPattern.UnitOfWork.EFCore.Models;

namespace RepositoyPattern.UnitOfWork.EFCore.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UserService> logger)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<bool> AddAsync(UserDto entity)
        {
            var item = mapper.Map<User>(entity);

            await unitOfWork.Users.AddAsync(item);
            await unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            await unitOfWork.Users.DeleteAsync(id);
            await unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<IEnumerable<UserDto>> FindAllAsync()
        {
            var items = await unitOfWork.Users.FindAllAsync();

            logger.LogDebug("<{methodName}>: found <{itemsCount}> users>",
                MethodBase.GetCurrentMethod().ReflectedType.Name,
                items.Count());

            return mapper.Map<IEnumerable<UserDto>>(items);
        }

        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            var item = await unitOfWork.Users.GetByIdAsync(id);

            logger.LogDebug("<{methodName}>: found <{user}> user>",
                MethodBase.GetCurrentMethod().ReflectedType.Name,
                item);

            return mapper.Map<UserDto>(item);
        }

        public async Task<bool> UpdateAsync(UserDto entity)
        {
            var item = mapper.Map<User>(entity);

            await unitOfWork.Users.UpdateAsync(item);
            await unitOfWork.CompleteAsync();

            return true;
        }
    }
}
