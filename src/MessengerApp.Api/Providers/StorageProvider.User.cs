using System;
using System.Threading.Tasks;
using MessengerApp.Api.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MessengerApp.Api.Providers
{
    public partial class StorageProvider
    {
        public async ValueTask<User> AddUser(User user)
        {
            EntityEntry<User> storageUser = await this.Users.AddAsync(user);
            await this.SaveChangesAsync();

            return storageUser.Entity;
        }
    }
}
