using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessengerApp.Api.Models;
using MessengerApp.Api.Providers;
using Microsoft.Extensions.Logging;

namespace MessengerApp.Api.Services
{
    public class MessageService : IMessageService
    {
        private readonly IStorageProvider storageProvider;
        private readonly ILogger<MessageService> logger;

        public MessageService(IStorageProvider storageProvider, ILogger<MessageService> logger)
        {
            this.storageProvider = storageProvider;
            this.logger = logger;
        }

        public IEnumerable<DirectMessage> GetUserConversation(Guid userId, Guid contactId)
        {
            logger.LogInformation($"retrieving conversation between user {userId} and user {contactId}");

            return storageProvider
                .SelectAllMessages()
                .Where(message => (message.RecipientId == userId && message.SenderId == contactId) ||
                    (message.RecipientId == contactId && message.SenderId == userId));
        }

        public async ValueTask<DirectMessage> SendMessageAsync(Guid userId, Guid contactId, string message)
        {
            logger.LogInformation($"sending message from user {userId} to user {contactId}");

            var dm = new DirectMessage
            {
                RecipientId = contactId,
                SenderId = userId,
                Text = message,
                Sent = DateTimeOffset.UtcNow
            };

            return await storageProvider.InsertMessageAsync(dm);
        }
    }
}
