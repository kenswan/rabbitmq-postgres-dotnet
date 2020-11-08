using System.Collections.Generic;
using System.Threading.Tasks;
using MessengerApp.Api.Models;
using MessengerApp.Api.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MessengerApp.Api.Controllers
{
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        private readonly IMessagingProvider messagingProvider;

        public MessageController(IMessagingProvider messagingProvider)
        {
            this.messagingProvider = messagingProvider;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async ValueTask<ActionResult<Message>> Post([FromBody] Message message)
        {
            var completedMessage = await this.messagingProvider.SendMessage(message);

            return Ok(completedMessage);
        }
    }
}
