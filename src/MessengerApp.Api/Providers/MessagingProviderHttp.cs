using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MessengerApp.Api.Models;
using Microsoft.Extensions.Options;

namespace MessengerApp.Api.Providers
{
    public class MessagingProviderHttp : IMessagingProvider
    {
        private readonly HttpClient httpClient;
        private readonly RabbitMQConfig rabbitMQConfig;

        public MessagingProviderHttp(IOptions<RabbitMQConfig> rabbitMQOptions, HttpClient httpClient)
        {
            this.httpClient = httpClient;
            rabbitMQConfig = rabbitMQOptions.Value ?? throw new ArgumentNullException(nameof(rabbitMQOptions));
        }

        public async ValueTask<Message> SendMessage(Message message)
        {
            var relativeUrl = $"api/exchanges/{rabbitMQConfig.VirtualHost}/amq.direct/publish";

            HttpResponseMessage httpResponseMessage =
                await httpClient.PostAsJsonAsync(relativeUrl, CreateRequest(rabbitMQConfig.Queue, message));

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var response = await httpResponseMessage.Content.ReadFromJsonAsync<RabbitMQResponse>();

                return (response.Routed == true) ? message : default;
            }
            else
                return default;
        }

        private RabbitMQRequest CreateRequest(string messageQueue, Message message) =>
            new RabbitMQRequest
            {
                Payload = message.Body,
                RoutingKey = messageQueue,
                PayloadEncoding = "string"
            };
    }
}
