using Microsoft.EntityFrameworkCore;
using TicketStore.Data.Model;

namespace TicketStore.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        public ApplicationContext() : base()
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var settings = new ApplicationSettings();
            options.UseNpgsql(settings.ConnectionString());
        }
    }
}