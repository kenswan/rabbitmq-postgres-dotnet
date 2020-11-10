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
            return storageProvider
                .SelectAllUsers()
                .Where(user => user.UserName == username)
                .FirstOrDefault();
        }

        public IEnumerable<User> GetUserContacts(Guid id)
        {
            return storageProvider
                .SelectAllMessages()
                .Where(message => message.RecipientId == id || message.SenderId == id) // Get converstations received or sent by user
                .Select(message => message.RecipientId == id ? message.Sender : message.Recipient); // return other party within message
        }

        public async ValueTask<User> RegisterUserAsync(string username)
        {
            var user = new User { UserName = username };

            return await storageProvider.InsertUserAsync(user);
        }
    }
}
