using System;
namespace MessengerApp.Api.Models
{
    public class SentMessage : Message
    {
        public DateTimeOffset Sent { get; set; }

        public SentMessage(DirectMessage directMessage)
        {
            Sender = directMessage.SenderId;
            Recipient = directMessage.RecipientId;
            Body = directMessage.Text;
            Sent = directMessage.Sent;
        }
    }
}
