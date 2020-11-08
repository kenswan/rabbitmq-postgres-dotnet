using System;
namespace MessengerApp.Api.Models
{
    public class Message
    {
        public Guid Recipient { get; set; }
        public Guid Sender { get; set; }
        public string Body { get; set; }
    }
}
