using System;
namespace MessengerApp.Blazor.Models
{
    public class Message
    {
        public string RecipientId { get; set; }
        public string SenderId { get; set; }
        public string Text { get; set; }
    }
}
