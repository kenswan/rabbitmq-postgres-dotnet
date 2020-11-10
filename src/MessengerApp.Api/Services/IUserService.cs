using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MessengerApp.Api.Models;

namespace MessengerApp.Api.Services
{
    public interface IUserService
    {
        ValueTask<User> RegisterUserAsync(string username);

        User GetUserByUsername(string username);

        IEnumerable<User> GetUserContacts(Guid id);
    }
}
