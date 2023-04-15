using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory.Infrastructure.Internal;
using TicketStore.Data.Model;

namespace TicketStore.Data
{
    public class ApplicationContext : DbContext
    {
        private DbContextOptions<ApplicationContext> _options;
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        // For migrations
        public ApplicationContext() : base()
        {
            _options = new DbContextOptions<ApplicationContext>();
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base()
        {
            _options = options;
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (IsAppRunning())
            {
                builder.UseNpgsql(new ApplicationSettings().ConnectionString());                
            }
            else
            {
                var inMemoryOptions = _options.GetExtension<InMemoryOptionsExtension>();
                builder.UseInMemoryDatabase(databaseName: inMemoryOptions.StoreName);
            }
        }

        private Boolean IsAppRunning()
        {
            try
            {
                var result = _options.GetExtension<InMemoryOptionsExtension>();
                return result == null;
            }
            catch (InvalidOperationException)
            {
                return true;
            }
        }
    }
}
