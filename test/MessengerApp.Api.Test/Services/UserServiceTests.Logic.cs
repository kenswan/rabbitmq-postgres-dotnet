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
            var expectedUser = GetUser(inputUserName);
            var userList = GetRandomUsers().ToList();
            userList.Add(expectedUser);

            storageProviderMock.Setup(service =>
                service.SelectAllUsers())
                .Returns(userList.AsQueryable());

            var actualUser = userService.GetUserByUsername(inputUserName);

            actualUser.Should().BeEquivalentTo(expectedUser);
        }

        [Fact]
        public void ShouldReturnNullIfUserNameNotFound()
        {
            var inputUserName = new Faker().Internet.UserName();
            var userList = GetRandomUsers();

            storageProviderMock.Setup(service =>
                service.SelectAllUsers())
                .Returns(userList.AsQueryable());

            var actualUser = userService.GetUserByUsername(inputUserName);

            actualUser.Should().BeNull();
        }

        [Fact]
        public async Task ShouldRegisterNewUser()
        {
            var inputUserName = new Faker().Internet.UserName();
            var createdUser = GetUser(inputUserName);

            storageProviderMock.Setup(service =>
                service.InsertUserAsync(It.Is<User>(user =>
                    user.UserName == inputUserName)))
                .ReturnsAsync(createdUser);

            var actualUser = await userService.RegisterUserAsync(inputUserName);

            actualUser.Should().BeEquivalentTo(createdUser);
        }
    }
}
