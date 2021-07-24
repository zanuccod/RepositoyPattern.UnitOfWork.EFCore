using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RepositoyPattern.UnitOfWork.EFCore.Core;
using RepositoyPattern.UnitOfWork.EFCore.Core.IRepositories;
using RepositoyPattern.UnitOfWork.EFCore.Core.Repository;
using RepositoyPattern.UnitOfWork.EFCore.Data;

namespace RepositoyPattern.UnitOfWork.EFCore
{
    public sealed class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger logger;

        public IUserRepository Users { get; private set; }

        public UnitOfWork(ApplicationDbContext context,
            IUserRepository userRepository,
            ILogger<UnitOfWork> logger)
        {
            this.context = context;
            this.logger = logger;

            Users = userRepository;
        }

        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
