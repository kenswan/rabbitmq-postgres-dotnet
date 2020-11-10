using System;
using System.Linq;
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

        private static DirectMessage GetMessage(Guid senderId, Guid recipientId, string message = null) =>
            GetMessageFake(senderId, recipientId, message).Generate();

        private static IQueryable<DirectMessage> GetMessages(Guid senderId, Guid recipientId, string message = null) =>
            GetMessageFake(senderId, recipientId, message)
            .Generate(new Faker().Random.Int(1, 3))
            .AsQueryable();

        private static Faker<DirectMessage> GetMessageFake(Guid senderId, Guid recipientId, string message = null) =>
            new Faker<DirectMessage>()
                .RuleFor(dm => dm.Id, faker => faker.Random.Guid())
                .RuleFor(dm => dm.RecipientId, recipientId)
                .RuleFor(dm => dm.SenderId, senderId)
                .RuleFor(dm => dm.Text, faker => message ?? faker.Lorem.Sentence())
                .RuleFor(dm => dm.Sent, DateTimeOffset.UtcNow);
    }
}
