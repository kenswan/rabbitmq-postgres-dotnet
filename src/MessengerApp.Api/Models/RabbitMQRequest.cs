using System.Text.Json.Serialization;
namespace MessengerApp.Api.Models
{
    public class RabbitMQRequest
    {
        [JsonPropertyName("routing_key")]
        public string RoutingKey { get; set; }

        public object Properties { get; set; }

        public string Payload { get; set; }

        [JsonPropertyName("payload_encoding")]
        public string PayloadEncoding { get; set; }
    }
}
