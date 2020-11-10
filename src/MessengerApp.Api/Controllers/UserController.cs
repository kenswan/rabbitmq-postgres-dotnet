using System.Threading.Tasks;
using MessengerApp.Api.Models;
using MessengerApp.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MessengerApp.Api.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        public readonly IUserService userService;
        public readonly ILogger<UserController> logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            this.userService = userService;
            this.logger = logger;
        }

        /// <summary>
        /// Retrieves user if existing or registers new user if not found
        /// </summary>
        /// <param name="username">Username of specified user</param>
        /// <returns>Existing user or new user with the given username<see cref="User"/></returns>
        [HttpPost("{username}/login")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async ValueTask<ActionResult<User>> Login([FromRoute] string username)
        {
            var user = userService.GetUserByUsername(username);

            user ??= await userService.RegisterUserAsync(username);

            return Ok(user);
        }
    }
}
