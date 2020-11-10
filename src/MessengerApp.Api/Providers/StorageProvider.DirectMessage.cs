using System.Linq;
using System.Threading.Tasks;
using MessengerApp.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MessengerApp.Api.Providers
{
    public partial class StorageProvider
    {
        public async ValueTask<DirectMessage> InsertMessageAsync(DirectMessage directMessage)
        {
            EntityEntry<DirectMessage> storageDirectMessage =
                await this.DirectMessages.AddAsync(directMessage);

            await this.SaveChangesAsync();

            return storageDirectMessage.Entity;
        }

        public IQueryable<DirectMessage> SelectAllMessages()
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            return this.DirectMessages;
        }
    }
}
