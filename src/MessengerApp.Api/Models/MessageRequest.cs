using System;
namespace MessengerApp.Api.Models
{
    public class MessageRequest
    {
        public Guid Recipient { get; set; }
        public string Text { get; set; }
    }
}
