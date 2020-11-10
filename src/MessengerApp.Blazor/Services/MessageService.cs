using System.Collections.Generic;
using System.Threading.Tasks;
using MessengerApp.Blazor.Models;

namespace MessengerApp.Blazor.Services
{
    public class MessageService : IMessageService
    {
        private readonly IRestClient restClient;

        public MessageService(IRestClient restClient)
        {
            this.restClient = restClient;
        }

        public async ValueTask<IEnumerable<Message>> GetMessagesAsync(string userId, string contactId) =>
            await restClient.GetContentAsync<IEnumerable<Message>>($"api/user/{userId}/message?contactId={contactId}");

        public async ValueTask<Message> SendMessageAsync(string userId, string contactId, string text) =>
            await restClient.PostContentAsync<Message>($"api/user/{userId}/message",
                new MessageRequest { Recipient = contactId, Text = text });
    }
}
