using Microsoft.EntityFrameworkCore;
using TicketStore.Api.Tests.Model;
using TicketStore.Api.Tests.Environment;

namespace TicketStore.Api.Tests.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        public ApplicationContext() : base(
            new DbContextOptionsBuilder<ApplicationContext>().UseNpgsql(
                    $"Host={new AppHost().Value()};Port=5432;Database=store;Username=store_user;Password=KqCQzyH2akGB9gQ4"
                ).Options
            ) { }
    }
}
