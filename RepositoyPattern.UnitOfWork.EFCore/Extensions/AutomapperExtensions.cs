using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using RepositoyPattern.UnitOfWork.EFCore.Mappers;

namespace RepositoyPattern.UnitOfWork.EFCore.Extensions
{
    public static class AutomapperExtensions
    {
        public static void AddAutomapperReference(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserProfile));
        }
    }
}
