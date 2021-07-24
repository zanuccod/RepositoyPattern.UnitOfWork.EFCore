using System;
using Microsoft.Extensions.DependencyInjection;
using RepositoyPattern.UnitOfWork.EFCore.Core;

namespace RepositoyPattern.UnitOfWork.EFCore.Extensions
{
    public static class UnitOfWorkExtensions
    {
        public static void AddUnitOfWorkReference(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
