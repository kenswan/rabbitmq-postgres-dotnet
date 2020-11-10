using System;
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
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        public async ValueTask<ActionResult<User>> Login([FromRoute] string username)
        {
            var user = userService.GetUserByUsername(username);

            user ??= await userService.RegisterUserAsync(username);

            return Ok(user);
        }

        /// <summary>
        /// Retrieves contacts from all conversations for a given user
        /// </summary>
        /// <param name="userId">User Id of specified user</param>
        /// <returns>List of contacts from all conversations the user has had</returns>
        [HttpGet("{userId}/contact")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<User> GetContacts([FromRoute] Guid userId)
        {
            var contacts = userService.GetUserContacts(userId);

            return Ok(contacts);
        }

        /// <summary>
        /// Retrieves user by a given username
        /// </summary>
        /// <param name="username">Username of specified user</param>
        /// <returns>User with the given username</returns>
        [HttpGet("{username}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<User> GetUser([FromRoute] string username)
        {
            var user = userService.GetUserByUsername(username);

            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }
}
