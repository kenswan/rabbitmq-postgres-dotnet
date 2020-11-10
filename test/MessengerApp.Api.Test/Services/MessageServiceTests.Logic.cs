using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using MessengerApp.Api.Models;
using Moq;
using Xunit;

namespace MessengerApp.Api.Test.Services
{
    public partial class MessageServiceTests
    {
        [Fact]
        public async Task ShouldSendMessage()
        {
            var senderId = Guid.NewGuid();
            var recipientId = Guid.NewGuid();
            var message = new Faker().Lorem.Sentence();

            var expectedMessage = GetMessage(senderId, recipientId, message);

            storageProviderMock.Setup(provider =>
                provider.InsertMessageAsync(It.Is<DirectMessage>(dm =>
                        dm.SenderId == senderId &&
                        dm.RecipientId == recipientId &&
                        dm.Text == message)))
                    .ReturnsAsync(expectedMessage);

            var actualMessage = await messageService.SendMessageAsync(senderId, recipientId, message);

            actualMessage.Should().BeEquivalentTo(expectedMessage);
        }

        [Fact]
        public void ShouldGetMessagesConversation()
        {
            var userId = Guid.NewGuid();
            var contactOne = Guid.NewGuid();
            var contactTwo = Guid.NewGuid();
            var contactThree = Guid.NewGuid();

            var contactOneMessages = GetMessages(userId, contactOne);
            var contactTwoMessages = GetMessages(userId, contactTwo);
            var contactThreeMessages = GetMessages(userId, contactThree);

            var compiledMessages = new List<DirectMessage>();
            compiledMessages.AddRange(contactOneMessages);
            compiledMessages.AddRange(contactTwoMessages);
            compiledMessages.AddRange(contactThreeMessages);

            storageProviderMock.Setup(provider =>
                provider.SelectAllMessages())
                    .Returns(compiledMessages.AsQueryable());

            var actualMessages = messageService.GetUserConversation(userId, contactTwo);

            actualMessages.Should().BeEquivalentTo(contactTwoMessages);
        }
    }
}
