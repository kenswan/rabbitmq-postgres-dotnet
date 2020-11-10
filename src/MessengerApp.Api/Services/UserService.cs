using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessengerApp.Api.Models;
using MessengerApp.Api.Providers;
using Microsoft.Extensions.Logging;

namespace MessengerApp.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IStorageProvider storageProvider;
        private readonly ILogger<UserService> logger;

        public UserService(IStorageProvider storageProvider, ILogger<UserService> logger)
        {
            this.storageProvider = storageProvider;
            this.logger = logger;
        }

        public User GetUserByUsername(string username)
        {
            logger.LogInformation($"retrieving user by username {username}");

            return storageProvider
                .SelectAllUsers()
                .Where(user => user.UserName == username)
                .FirstOrDefault();
        }

        public IEnumerable<User> GetUserContacts(Guid id)
        {
            logger.LogInformation($"retrieving contacts for user {id}");

            return storageProvider
                .SelectAllMessages()
                .Where(message => message.RecipientId == id || message.SenderId == id) // Get converstations received or sent by user
                .Select(message => message.RecipientId == id ? message.Sender : message.Recipient) // return other party within message
                .ToList()
                .Distinct(new UserComparer());
        }

        public async ValueTask<User> RegisterUserAsync(string username)
        {
            logger.LogInformation($"register new user {username}");

            var user = new User { UserName = username };

            return await storageProvider.InsertUserAsync(user);
        }
    }
}
