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
    public partial class UserServiceTests
    {
        [Fact]
        public void ShouldGetExistingUserByUserName()
        {
            var inputUserName = new Faker().Internet.UserName();
            var expectedUser = GetUser(id: null, username: inputUserName);
            var userList = GetRandomUsers().ToList();
            userList.Add(expectedUser);

            storageProviderMock.Setup(provider =>
                provider.SelectAllUsers())
                .Returns(userList.AsQueryable());

            var actualUser = userService.GetUserByUsername(inputUserName);

            actualUser.Should().BeEquivalentTo(expectedUser);
        }

        [Fact]
        public void ShouldReturnNullIfUserNameNotFound()
        {
            var inputUserName = new Faker().Internet.UserName();
            var userList = GetRandomUsers();

            storageProviderMock.Setup(provider =>
                provider.SelectAllUsers())
                .Returns(userList.AsQueryable());

            var actualUser = userService.GetUserByUsername(inputUserName);

            actualUser.Should().BeNull();
        }

        [Fact]
        public async Task ShouldRegisterNewUser()
        {
            var inputUserName = new Faker().Internet.UserName();
            var createdUser = GetUser(id: null, username: inputUserName);

            storageProviderMock.Setup(provider =>
                provider.InsertUserAsync(It.Is<User>(user =>
                    user.UserName == inputUserName)))
                .ReturnsAsync(createdUser);

            var actualUser = await userService.RegisterUserAsync(inputUserName);

            actualUser.Should().BeEquivalentTo(createdUser);
        }

        [Fact]
        public void ShouldGetUserContacts()
        {
            var inputUser = GetUser();
            var contactOne = GetUser();
            var contactTwo = GetUser();
            var contactThree = GetUser();

            var expectedContacts = new List<User> { contactOne, contactTwo, contactThree };

            var messageList = new List<DirectMessage>
            {
                GetDirectMessage(inputUser, contactOne),
                GetDirectMessage(inputUser, contactTwo),
                GetDirectMessage(inputUser, contactThree),
            };

            storageProviderMock.Setup(provider =>
                provider.SelectAllMessages())
                .Returns(messageList.AsQueryable());

            var actualContacts = userService.GetUserContacts(inputUser.Id);

            actualContacts.Should().BeEquivalentTo(expectedContacts);
        }
    }
}
