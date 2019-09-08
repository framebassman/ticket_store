using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using TicketStore.Api.Tests.Unit.TestData;
using TicketStore.Data;
using TicketStore.Data.Model;

namespace TicketStore.Api.Tests.Unit.BaseTest
{
    public abstract class DbBaseTest<T> : IDisposable
    {
        protected readonly ApplicationContext Db;
        protected readonly DbContextOptions<ApplicationContext> Options;
        
        protected readonly ILogger<T> Logger;
        protected readonly Provider Provider;

        protected Merchant _merchant;
        protected Event _concert;
        protected List<Ticket> _tickets;
        protected Payment _payment;

        public DbBaseTest(String databaseName)
        {
            Options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;
            Logger = new Mock<ILogger<T>>().Object;
            Provider = new Provider();
            Db = new ApplicationContext(Options);
        }

        protected void SeedTestData(DateTime date)
        {
            CleanUpDatabase();

            var merchant = Provider.Merchants().First();
            _merchant = Db.Merchants.Add(merchant).Entity;

            var concert = Provider.Events(_merchant).WithDate(date);
            _concert = Db.Events.Add(concert).Entity;

            var tickets = Provider.Tickets(_concert).List();
            _tickets = tickets;
            Db.Tickets.AddRange(_tickets);

            _payment = Provider.Payments(_tickets).First();
            Db.Payments.Add(_payment);

            Db.SaveChanges();
        }

        protected void CleanUpDatabase()
        {
            Db.Merchants.RemoveRange(Db.Merchants);
            Db.Events.RemoveRange(Db.Events);
            Db.Tickets.RemoveRange(Db.Tickets);
            Db.Payments.RemoveRange(Db.Payments);
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
