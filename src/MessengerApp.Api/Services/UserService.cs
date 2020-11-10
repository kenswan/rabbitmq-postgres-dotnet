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
            throw new NotImplementedException();
        }

        public async ValueTask<User> RegisterUserAsync(string username)
        {
            var user = new User { UserName = username };

            return await storageProvider.InsertUserAsync(user);
        }
    }
}
