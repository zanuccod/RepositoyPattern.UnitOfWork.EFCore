using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RepositoyPattern.UnitOfWork.EFCore.Controllers;

namespace RepositoyPattern.UnitOfWork.EFCore.IntegrationTest
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration)
            : base(configuration)
        { }

        protected override void ConfigureApplicationDbContext(IServiceCollection services)
        {
            // add this to prevent "404-NotFound" error when controller are on separate assembly
            // add an entry for every controller
            services.AddControllers().AddApplicationPart(typeof(UserController).Assembly);
        }
    }
}
