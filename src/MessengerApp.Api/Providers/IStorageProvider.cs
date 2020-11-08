using System;
using System.Threading.Tasks;
using MessengerApp.Api.Models;

namespace MessengerApp.Api.Providers
{
    public interface IStorageProvider
    {
        ValueTask<User> AddUser(User user);
    }
}
