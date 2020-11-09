using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MessengerApp.Blazor.Models;

namespace MessengerApp.Blazor.Services
{
    public class MessageService : IMessageService
    {
        public MessageService()
        {
        }

        public ValueTask<IEnumerable<Contact>> GetContactsAsync(string userId)
        {
            var contacts = new List<Contact>
            {
                new Contact { Id = "123456", UserName = "kswan1" },
                new Contact { Id = "123457", UserName = "kswan2" },
                new Contact { Id = "123458", UserName = "kswan3" },
            };

            return new ValueTask<IEnumerable<Contact>>(contacts);
        }

        public ValueTask<IEnumerable<Message>> GetMessagesAsync(string userId, string contactId)
        {
            var messages = new List<Message>
            {
                new Message { Body = "this is test", Sender = "You" },
                new Message { Body = "this is test 2", Sender = "Other" },
                new Message { Body = "this is test 3", Sender = "You" },
                new Message { Body = "this is test 4", Sender = "Other" },
                new Message { Body = "this is test 5", Sender = "You" },
                new Message { Body = "this is test 6", Sender = "Other" },
            };

            return new ValueTask<IEnumerable<Message>>(messages);
        }

        public ValueTask<Message> SendMessageAsync(Message message)
        {
            return new ValueTask<Message>(message);
        }
    }
}
