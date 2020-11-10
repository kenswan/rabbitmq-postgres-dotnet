using System.Collections.Generic;
using System.Threading.Tasks;
using MessengerApp.Blazor.Models;

namespace MessengerApp.Blazor.Services
{
    public interface IUserService
    {
        ValueTask<Contact> LogInUserByUserNameAsync(string username);

        ValueTask<IEnumerable<Contact>> GetContactsAsync(string userId);

        ValueTask<Contact> GetUserAsync(string username);
    }
}
