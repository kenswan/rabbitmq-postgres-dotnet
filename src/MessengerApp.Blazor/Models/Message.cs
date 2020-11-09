using System;
namespace MessengerApp.Blazor.Models
{
    public class Message
    {
        public string Recipient { get; set; }
        public string Sender { get; set; }
        public string Body { get; set; }
    }
}
