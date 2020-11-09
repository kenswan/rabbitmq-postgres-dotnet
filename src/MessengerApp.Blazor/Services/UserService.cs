using System;
using System.Threading.Tasks;
using MessengerApp.Blazor.Models;

namespace MessengerApp.Blazor.Services
{
    public class UserService : IUserService
    {
        public UserService()
        {
        }

        public ValueTask<Contact> LogInUserByUserNameAsync(string username)
        {
            return new ValueTask<Contact>(new Contact { Id = Guid.NewGuid().ToString(), UserName = username });
        }
    }
}
