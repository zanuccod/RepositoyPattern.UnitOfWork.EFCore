using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using RepositoyPattern.UnitOfWork.EFCore.Data;
using RepositoyPattern.UnitOfWork.EFCore.Dto;
using Xunit;
using RepositoyPattern.UnitOfWork.EFCore.Models;
using System.Linq;
using Newtonsoft.Json;

namespace RepositoyPattern.UnitOfWork.EFCore.IntegrationTest.Controller
{
    public class UserControllerTest
    {
        private readonly HttpClient client;
        private readonly string ServerBaseAddress = "https://localhost:5001";

        private ApplicationDbContext applicationDbContext;


        public UserControllerTest()
        {
            var options = new DbContextOptionsBuilder()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            applicationDbContext = new ApplicationDbContext(options);

            var server = new TestServer(new WebHostBuilder()
                    .UseStartup<TestStartup>()
                    .ConfigureServices(
                        services =>
                        {
                            services.AddScoped(x => { return applicationDbContext; });
                        })
                    .UseDefaultServiceProvider(x => x.ValidateScopes = false)
                    .UseKestrel());

            client = server.CreateClient();
        }

        [Fact]
        public async Task FindAll_ShouldReturnListOfUserDto()
        {
            // Act
            var response = await client.GetAsync(string.Join("/", ServerBaseAddress, "api/user/find-all"));

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var returnedList = JsonConvert.DeserializeObject<IEnumerable<UserDto>>(await response.Content.ReadAsStringAsync());

            Assert.Empty(returnedList);
        }

        [Fact]
        public async Task FindAll_ShouldReturnAllElementsOnDatabase()
        {
            // Arrange
            var user = new User()
            {
                Id = 1,
                FirstName = "Pippo",
                LastName = "Paperino",
                Email = "pippo.paperino@disney.it"
            };

            await applicationDbContext.Users.AddAsync(user);
            await applicationDbContext.SaveChangesAsync();

            // Act
            var response = await client.GetAsync(string.Join("/", ServerBaseAddress, "api/user/find-all"));

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var returnedList = JsonConvert.DeserializeObject<IEnumerable<UserDto>>(await response.Content.ReadAsStringAsync());

            Assert.NotEmpty(returnedList);
            Assert.Single(returnedList);
            Assert.Equal(user.Id, returnedList.First().Id);
            Assert.Equal(user.FirstName, returnedList.First().FirstName);
            Assert.Equal(user.LastName, returnedList.First().LastName);
            Assert.Equal(user.Email, returnedList.First().Email);
        }
    }
}
