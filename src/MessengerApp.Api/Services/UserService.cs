using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetUserContacts(Guid id)
        {
            throw new NotImplementedException();
        }

        public ValueTask<User> RegisterUserAsync(string username)
        {
            throw new NotImplementedException();
        }
    }
}
