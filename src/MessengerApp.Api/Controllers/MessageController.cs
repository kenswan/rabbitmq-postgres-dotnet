using System.Collections.Generic;
using MessengerApp.Api.Models;
using MessengerApp.Api.Providers;
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
        public void Post([FromBody] string value)
        {
            this.messagingProvider.SendMessage(new Message { Body = value });
        }
    }
}
