using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        public ValueTask<SentMessage> SendMessageAsync(Guid userId, Guid contactId, string message)
        {
            throw new NotImplementedException();
        }
    }
}
