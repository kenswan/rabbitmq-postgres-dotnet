using Microsoft.EntityFrameworkCore;

namespace MessengerApp.Api.Providers
{
    public class StorageProvider : DbContext
    {
        public StorageProvider(DbContextOptions<StorageProvider> options)
            : base(options) { }
    }
}
