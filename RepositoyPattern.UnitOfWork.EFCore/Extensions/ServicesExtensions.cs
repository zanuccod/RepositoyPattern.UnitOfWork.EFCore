using System;
using Microsoft.Extensions.DependencyInjection;
using RepositoyPattern.UnitOfWork.EFCore.Services;

namespace RepositoyPattern.UnitOfWork.EFCore.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddServicesReference(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }
    }
}
