using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MessengerApp.Api.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string UserName { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<DirectMessage> ReceivedMessages { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<DirectMessage> SentMessages { get; set; }
    }
}
