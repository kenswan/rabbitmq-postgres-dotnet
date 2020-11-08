using System.Threading.Tasks;
using MessengerApp.Api.Models;

namespace MessengerApp.Api.Providers
{
    public interface IMessagingProvider
    {
        ValueTask<Message> SendMessage(Message message);
    }
}
