using System;
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
                    $"Host={Host()};Port=5432;Database=store_db;Username=store_user;Password=KqCQzyH2akGB9gQ4"
                ).Options
            ) { }

        private static String Host()
        {
            var host = new AppHost();
            if (host.InsideDockerContainer())
            {
                return "postgres";
            }
            else
            {
                return host.Value();
            }
        }
    }
}
