using System.Collections.Generic;
using System.Threading.Tasks;
using MessengerApp.Blazor.Models;

namespace MessengerApp.Blazor.Services
{
    public interface IMessageService
    {
        ValueTask<IEnumerable<Contact>> GetContactsAsync(string userId);

        ValueTask<IEnumerable<Message>> GetMessagesAsync(string userId, string contactId);

        ValueTask<Message> SendMessageAsync(Message message);
    }
}
