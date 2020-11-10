using System;
using System.Linq;
using System.Threading.Tasks;
using MessengerApp.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MessengerApp.Api.Providers
{
    public partial class StorageProvider
    {
        public async ValueTask<User> InsertUserAsync(User user)
        {
            EntityEntry<User> storageUser = await this.Users.AddAsync(user);
            await this.SaveChangesAsync();

            return storageUser.Entity;
        }

        public IQueryable<User> SelectAllUsers()
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            return this.Users;
        }

        public async ValueTask<User> SelectUserByIdAsync(Guid id)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            return await this.Users.FindAsync(id);
        }
    }
}
