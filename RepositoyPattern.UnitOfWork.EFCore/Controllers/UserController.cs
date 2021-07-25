using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RepositoyPattern.UnitOfWork.EFCore.Models;
using RepositoyPattern.UnitOfWork.EFCore.Services;
using RepositoyPattern.UnitOfWork.EFCore.Dto;

namespace RepositoyPattern.UnitOfWork.EFCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly ILogger logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            this.userService = userService;
            this.logger = logger;
        }

        [HttpGet]
        [Route("find-all")]
        public async Task<ActionResult<IEnumerable<User>>> FindAll()
        {
            try
            {
                var items = await userService
                    .FindAllAsync();

                logger.LogDebug("<{endPointName}>: found <{itemsCount}> items",
                    MethodBase.GetCurrentMethod().ReflectedType.Name,
                    items.Count());

                return new OkObjectResult(items);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to process end-point <{functionName}>: <{errorMsg}>", MethodBase.GetCurrentMethod().ReflectedType.Name, ex);
                return new ObjectResult($"Failed to process end-point <{MethodBase.GetCurrentMethod().ReflectedType.Name}>: {ex.Message}") { StatusCode = 500 };
            }
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult<IEnumerable<User>>> Add(UserDto userDto)
        {
            try
            {
                var item = await userService
                    .AddAsync(userDto);

                if (item)
                {
                    logger.LogDebug("<{endPointName}>: Added <{userDto}>",
                        MethodBase.GetCurrentMethod().ReflectedType.Name,
                        item);
                }
                else
                {
                    logger.LogDebug("<{endPointName}>: Unable to add <{userDto}>",
                        MethodBase.GetCurrentMethod().ReflectedType.Name,
                        item);
                }

                return new OkResult();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to process end-point <{functionName}>: <{errorMsg}>", MethodBase.GetCurrentMethod().ReflectedType.Name, ex);
                return new ObjectResult($"Failed to process end-point <{MethodBase.GetCurrentMethod().ReflectedType.Name}>: {ex.Message}") { StatusCode = 500 };
            }
        }
    }
}
