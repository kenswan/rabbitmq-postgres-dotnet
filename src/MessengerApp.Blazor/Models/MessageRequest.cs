using System;
namespace MessengerApp.Blazor.Models
{
    public class MessageRequest
    {
        public string Recipient { get; set; }
        public string Text { get; set; }
    }
}
