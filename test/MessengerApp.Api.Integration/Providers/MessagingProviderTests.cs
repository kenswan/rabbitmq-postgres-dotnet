using System.Threading.Tasks;
using MessengerApp.Api.Models;
using MessengerApp.Api.Providers;
using Xunit;

namespace MessengerApp.Api.Integration.Providers
{
    public class MessagingProviderTests
    {
        private RabbitMQConfig rabbitMQConfig;

        private readonly IMessagingProvider messagingProvider;

        public MessagingProviderTests()
        {
            rabbitMQConfig = new RabbitMQConfig { HostName = "localhost" };

            messagingProvider = new MessagingProviderClient(rabbitMQConfig);
        }

        [Fact(DisplayName = "Integration: should send message to queue")]
        public async Task ShouldSendMessage()
        {
            // RabbitMQ.Client.Exceptions.BrokerUnreachableException will throw if no connection
            var message = await messagingProvider.SendMessage(new Message { Body = "Test Message" });

            Assert.NotNull(message);
        }
    }
}
