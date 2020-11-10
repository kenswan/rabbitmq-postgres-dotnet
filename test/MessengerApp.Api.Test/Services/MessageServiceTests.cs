using System;
using Bogus;
using MessengerApp.Api.Models;
using MessengerApp.Api.Providers;
using MessengerApp.Api.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace MessengerApp.Api.Test.Services
{
    public partial class MessageServiceTests
    {
        private readonly Mock<IStorageProvider> storageProviderMock;
        private readonly Mock<ILogger<MessageService>> loggerMock;

        private readonly IMessageService messageService;

        public MessageServiceTests()
        {
            storageProviderMock = new Mock<IStorageProvider>();
            loggerMock = new Mock<ILogger<MessageService>>();

            messageService = new MessageService(storageProviderMock.Object, loggerMock.Object);
        }

        private static DirectMessage GetMessage(Guid senderId, Guid recipientId, string message) =>
            new Faker<DirectMessage>()
                .RuleFor(dm => dm.Id, faker => faker.Random.Guid())
                .RuleFor(dm => dm.RecipientId, recipientId)
                .RuleFor(dm => dm.SenderId, senderId)
                .RuleFor(dm => dm.Text, faker => message)
                .RuleFor(dm => dm.Sent, DateTimeOffset.UtcNow)
                .Generate();

    }
}
