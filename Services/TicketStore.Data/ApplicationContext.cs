using System;
using Microsoft.EntityFrameworkCore;
using TicketStore.Data.Model;

namespace TicketStore.Data
{
    public class ApplicationContext : DbContext
    {
        private readonly DbContextOptions<ApplicationContext> _options;
        private readonly Boolean _isInUnitTests;
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        // This constructor should be used in production code
        public ApplicationContext() : base()
        {
            _isInUnitTests = false;
        }

        // This constructor should be used in unit tests
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            _isInUnitTests = true;
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (IsAppRunning())
            {
                builder.UseNpgsql(new ApplicationSettings().ConnectionString());                
            }
        }

        private Boolean IsAppRunning()
        {
            return !_isInUnitTests;
        }
    }
}
