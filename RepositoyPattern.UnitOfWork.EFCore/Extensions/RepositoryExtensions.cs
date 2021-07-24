using System;
using Microsoft.Extensions.DependencyInjection;
using RepositoyPattern.UnitOfWork.EFCore.Core.IRepositories;
using RepositoyPattern.UnitOfWork.EFCore.Core.Repository;

namespace RepositoyPattern.UnitOfWork.EFCore.Extensions
{
    public static class RepositoryExtensions
    {
        public static void AddRepositoryReference(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
