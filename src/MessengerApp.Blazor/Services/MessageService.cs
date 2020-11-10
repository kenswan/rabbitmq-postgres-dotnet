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
            await restClient.GetContent<IEnumerable<Message>>($"api/user/{userId}/message?contactId={contactId}");

        public ValueTask<Message> SendMessageAsync(Message message)
        {
            return new ValueTask<Message>(message);
        }
    }
}
