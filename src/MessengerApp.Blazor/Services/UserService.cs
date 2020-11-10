﻿using System.Collections.Generic;
using System.Threading.Tasks;
using MessengerApp.Blazor.Models;

namespace MessengerApp.Blazor.Services
{
    public class UserService : IUserService
    {
        private readonly IRestClient restClient;

        public UserService(IRestClient restClient)
        {
            this.restClient = restClient;
        }

        public async ValueTask<Contact> LogInUserByUserNameAsync(string username) =>
            await restClient.PostContent<Contact>($"api/user/{username}/login", null);

        public async ValueTask<IEnumerable<Contact>> GetContactsAsync(string userId) =>
            await restClient.GetContent<IEnumerable<Contact>>($"api/user/{userId}/contact");
    }
}
