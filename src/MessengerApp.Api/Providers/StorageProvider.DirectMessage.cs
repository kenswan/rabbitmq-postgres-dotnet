using System;
using System.Threading.Tasks;
using MessengerApp.Api.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MessengerApp.Api.Providers
{
    public partial class StorageProvider
    {
        public async ValueTask<DirectMessage> AddMessage(DirectMessage directMessage)
        {
            EntityEntry<DirectMessage> storageDirectMessage =
                await this.DirectMessages.AddAsync(directMessage);

            await this.SaveChangesAsync();

            return storageDirectMessage.Entity;
        }
    }
}
