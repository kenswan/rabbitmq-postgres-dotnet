using System.Threading.Tasks;
using MessengerApp.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace MessengerApp.Api.Providers
{
    public partial class StorageProvider : DbContext, IStorageProvider
    {
        public DbSet<DirectMessage> DirectMessages { get; set; }
        public DbSet<User> Users { get; set; }

        public StorageProvider(DbContextOptions<StorageProvider> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasMany<DirectMessage>(user => user.ReceivedMessages)
                .WithOne(dm => dm.Recipient);

            builder.Entity<User>()
                .HasMany<DirectMessage>(user => user.SentMessages)
                .WithOne(dm => dm.Sender);
        }
    }
}
