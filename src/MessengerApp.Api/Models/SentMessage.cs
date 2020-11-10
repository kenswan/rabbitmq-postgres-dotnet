using System;
namespace MessengerApp.Api.Models
{
    public class SentMessage
    {
        public Guid RecipientId { get; set; }
        public Guid SenderId { get; set; }
        public string Text { get; set; }
        public DateTimeOffset Sent { get; set; }
    }
}
