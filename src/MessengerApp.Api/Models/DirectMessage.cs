using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MessengerApp.Api.Models
{
    public class DirectMessage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Text { get; set; }

        public DateTimeOffset Sent { get; set; }

        public Guid RecipientId { get; set; }
        public virtual User Recipient { get; set; }

        public Guid SenderId { get; set; }
        public virtual User Sender { get; set; }
    }
}
