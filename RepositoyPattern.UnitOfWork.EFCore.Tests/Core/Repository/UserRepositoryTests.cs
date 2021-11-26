using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using RepositoyPattern.UnitOfWork.EFCore.Core.IRepositories;
using RepositoyPattern.UnitOfWork.EFCore.Core.Repository;
using RepositoyPattern.UnitOfWork.EFCore.Data;
using RepositoyPattern.UnitOfWork.EFCore.Models;
using Xunit;

namespace RepositoyPattern.UnitOfWork.EFCore.Tests.Core.Repository
{
    public class UserRepositoryTests
    {
        private IUserRepository userRepository;
        private ApplicationDbContext dbContext;

        public UserRepositoryTests()
        {
            var dbContextOptions = new DbContextOptionsBuilder()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            dbContext = new ApplicationDbContext(dbContextOptions);

            userRepository = new UserRepository(dbContext, new NullLogger<UserRepository>());
        }

        [Fact]
        public async Task AddAsync_ValidUser_ShouldAddNewUser()
        {
            // Arrange
            var user = GetMockUser();

            // Act
            await userRepository.AddAsync(user);
            await dbContext.SaveChangesAsync();

            // Assert
            var insertedUser = await dbContext
                .Users
                .FirstOrDefaultAsync(x => x.Id == 1);

            Assert.NotNull(insertedUser);
            Assert.Equal(user, insertedUser);
        }

        [Fact]
        public async Task UpdateAsync_ValidUser_ShouldUpdateUserFields()
        {
            // Arrange
            var user = GetMockUser();

            await dbContext
                .Users
                .AddAsync(user);
            await dbContext
                .SaveChangesAsync();

            user.FirstName = "firstName_1";
            user.LastName = "lastName_1";
            user.Email = "user_1@mail.yy";

            // Act
            await userRepository
                .UpdateAsync(user);

            // Assert
        }

        private User GetMockUser()
        {
            return new User()
            {
                Email = "user@mail.xx",
                FirstName = "firstName",
                LastName = "lastName"
            };
        }
    }
}

