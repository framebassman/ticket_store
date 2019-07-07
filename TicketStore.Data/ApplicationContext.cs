using System;
using Microsoft.EntityFrameworkCore;
using TicketStore.Data.Model;

namespace TicketStore.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        public ApplicationContext() : base()
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var settings = new ApplicationSettings();
            var connectionString = settings.ConnectionString();
            Console.WriteLine("Used ConnectionString: {0}", connectionString);
            options.UseNpgsql(connectionString);
        }
    }
}