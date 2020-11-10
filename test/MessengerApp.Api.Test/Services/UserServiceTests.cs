using System;
using System.Collections.Generic;
using Bogus;
using MessengerApp.Api.Models;
using MessengerApp.Api.Providers;
using MessengerApp.Api.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace MessengerApp.Api.Test.Services
{
    public partial class UserServiceTests
    {
        private readonly Mock<IStorageProvider> storageProviderMock;
        private readonly Mock<ILogger<UserService>> loggerMock;

        private readonly IUserService userService;

        public UserServiceTests()
        {
            storageProviderMock = new Mock<IStorageProvider>();
            loggerMock = new Mock<ILogger<UserService>>();

            userService = new UserService(storageProviderMock.Object, loggerMock.Object);
        }

        private static IEnumerable<User> GetRandomUsers() =>
            new Faker<User>()
                .RuleFor(user => user.Id, faker => faker.Random.Guid())
                .RuleFor(user => user.UserName, faker => faker.Internet.UserName())
                .Generate(GetRandomNumber());

        private static User GetUser(Guid? id = null, string username = null) =>
            new Faker<User>()
                .RuleFor(user => user.Id, faker => id ?? faker.Random.Guid())
                .RuleFor(user => user.UserName, faker => username ?? faker.Internet.UserName())
                .Generate();

        private static DirectMessage GetDirectMessage(User sender, User recipient) =>
            new Faker<DirectMessage>()
                .RuleFor(message => message.Id, faker => faker.Random.Guid())
                .RuleFor(message => message.SenderId, sender.Id)
                .RuleFor(message => message.Sender, sender)
                .RuleFor(message => message.RecipientId, recipient.Id)
                .RuleFor(message => message.Recipient, recipient)
                .RuleFor(message => message.Text, faker => faker.Lorem.Sentence())
                .Generate();

        private static int GetRandomNumber() =>
            new Faker().Random.Int(3, 5);
    }
}
