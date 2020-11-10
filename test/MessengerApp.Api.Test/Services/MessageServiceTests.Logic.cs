using System;
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
    }
}
