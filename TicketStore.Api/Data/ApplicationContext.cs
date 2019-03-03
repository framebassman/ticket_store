using Microsoft.EntityFrameworkCore;
using TicketStore.Api.Model;

namespace TicketStore.Api.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }
    }
}