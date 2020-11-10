using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MessengerApp.Api.Models;

namespace MessengerApp.Api.Services
{
    public interface IMessageService
    {
        IEnumerable<DirectMessage> GetUserConversation(Guid userId, Guid contactId);

        ValueTask<DirectMessage> SendMessageAsync(Guid userId, Guid contactId, string message);
    }
}
