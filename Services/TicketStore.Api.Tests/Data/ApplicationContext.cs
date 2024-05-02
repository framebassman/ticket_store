using Microsoft.EntityFrameworkCore;
using TicketStore.Api.Tests.Environment;
using TicketStore.Api.Tests.Model.Db;

namespace TicketStore.Api.Tests.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        public ApplicationContext() : base(
            new DbContextOptionsBuilder<ApplicationContext>().UseNpgsql(
                    // $"Host={new AppHost().Value()};Port=5432;Database=store_db;Username=store_user;Password=KqCQzyH2akGB9gQ4"
                    $"Host=ticket-store-romashov-tech.e.aivencloud.com;Port=20688;Database=test_store_db;Username=avnadmin;Password=AVNS_ZIb5nwfzb9X76Oit-l7"
                ).Options
            ) { }
    }
}
