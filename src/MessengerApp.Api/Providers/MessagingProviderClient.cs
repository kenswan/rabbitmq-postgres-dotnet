using System;
using System.Text;
using System.Threading.Tasks;
using MessengerApp.Api.Models;
using RabbitMQ.Client;

namespace MessengerApp.Api.Providers
{
    public class MessagingProviderClient : IMessagingProvider
    {
        private readonly RabbitMQConfig rabbitMQConfig;

        public MessagingProviderClient(RabbitMQConfig rabbitMQConfig)
        {
            this.rabbitMQConfig = rabbitMQConfig;
        }

        public ValueTask<Message> SendMessage(Message message)
        {
            var factory = new ConnectionFactory()
            {
                UserName = this.rabbitMQConfig.UserName,
                Password = this.rabbitMQConfig.Password,
                HostName = this.rabbitMQConfig.HostName,
                VirtualHost = this.rabbitMQConfig.VirtualHost,
                Port = this.rabbitMQConfig.Port,
                ContinuationTimeout = TimeSpan.MaxValue
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: rabbitMQConfig.Queue,
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    string testmessage = "Testing RMQ Connection";
                    var body = Encoding.UTF8.GetBytes(testmessage);

                    channel.BasicPublish(exchange: "",
                                         routingKey: rabbitMQConfig.Queue,
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine(" [x] Sent {0}", message);
                }
            }

            return new ValueTask<Message>(Task.FromResult(message));
        }
    }
}
