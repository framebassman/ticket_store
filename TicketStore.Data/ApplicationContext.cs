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
            var connectionString =
                "Host=postgres;Port=5432;Database=store_db;Username=store_user;Password=KqCQzyH2akGB9gQ4";
            options.UseNpgsql(connectionString);
        }
    }
}