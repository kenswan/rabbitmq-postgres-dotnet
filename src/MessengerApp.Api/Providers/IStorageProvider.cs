using System;
using System.Linq;
using System.Threading.Tasks;
using MessengerApp.Api.Models;

namespace MessengerApp.Api.Providers
{
    public interface IStorageProvider
    {
        ValueTask<User> InsertUserAsync(User user);

        ValueTask<DirectMessage> InsertMessageAsync(DirectMessage directMessage);

        IQueryable<DirectMessage> SelectAllMessages();

        IQueryable<User> SelectAllUsers();

        ValueTask<User> SelectUserByIdAsync(Guid id);
    }
}
