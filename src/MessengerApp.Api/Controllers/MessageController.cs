using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MessengerApp.Api.Models;
using MessengerApp.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MessengerApp.Api.Controllers
{
    [Route("api")]
    public class MessageController : Controller
    {
        private readonly IMessageService messageService;
        private readonly ILogger<MessageController> logger;

        public MessageController(
            IMessageService messageService,
            ILogger<MessageController> logger)
        {
            this.messageService = messageService;
            this.logger = logger;
        }

        /// <summary>
        /// Sends a direct message from one user to another
        /// </summary>
        /// <param name="userId">Sender's unique user Id</param>
        /// <param name="messageRequest">Message recipient and text sent from user</param>
        /// <returns>Message confirmation with timestamp <see cref="SentMessage"/></returns>
        [HttpPost("user/{userId}/message")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SentMessage), StatusCodes.Status200OK)]
        public async ValueTask<ActionResult<SentMessage>> PostMessage(
            [FromRoute] Guid userId,
            [FromBody] MessageRequest messageRequest)
        {
            logger?.LogInformation($"User {userId} sending message to {messageRequest.Recipient}");

            var sentMessage =
                await messageService.SendMessageAsync(userId, messageRequest.Recipient, messageRequest.Text);

            return Ok(sentMessage);
        }

        /// <summary>
        /// Retrieves direct messages (conversation) between two users
        /// </summary>
        /// <param name="userId">Unique ID of current user</param>
        /// <param name="contactId">Unique ID of user's contact in conversation</param>
        /// <returns>List of direct messages between user and contact</returns>
        [HttpGet("user/{userId}/message")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SentMessage), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<DirectMessage>> GetConversationWithContact(
            [FromRoute] Guid userId,
            [FromQuery, Required] Guid contactId)
        {
            logger?.LogInformation($"User {userId} requesting messages with {contactId}");

            var messages = messageService.GetUserConversation(userId, contactId);

            return Ok(messages);
        }
    }
}
