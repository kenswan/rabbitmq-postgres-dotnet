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
        private readonly IStorageProvider storageProvider;

        public MessageController(
            IMessagingProvider messagingProvider,
            IStorageProvider storageProvider)
        {
            this.messagingProvider = messagingProvider;
            this.storageProvider = storageProvider;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async ValueTask<ActionResult<SentMessage>> Post([FromBody] Message message)
        {
            // var completedMessage = await this.messagingProvider.SendMessage(message);

            var newMessage = new DirectMessage
            {
                RecipientId = message.Recipient,
                SenderId = message.Sender,
                Text = message.Body
            };

            var sentMessage = await this.storageProvider.AddMessage(newMessage);

            return Ok(new SentMessage(sentMessage));
        }
    }
}
