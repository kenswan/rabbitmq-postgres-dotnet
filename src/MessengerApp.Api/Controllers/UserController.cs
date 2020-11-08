using System.Threading.Tasks;
using MessengerApp.Api.Models;
using MessengerApp.Api.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MessengerApp.Api.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        public readonly IStorageProvider storageProvider;

        public UserController(IStorageProvider storageProvider)
        {
            this.storageProvider = storageProvider;
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async ValueTask<ActionResult<User>> Post([FromBody] User user)
        {
            var newUser = await storageProvider.AddUser(user);

            return Ok(newUser);
        }
    }
}
