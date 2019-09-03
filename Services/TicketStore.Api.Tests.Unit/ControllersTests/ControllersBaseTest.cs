using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using TicketStore.Api.Tests.Unit.TestData;
using TicketStore.Data;
using TicketStore.Data.Model;

namespace TicketStore.Api.Tests.Unit.ControllersTests
{
    public abstract class ControllersBaseTest<T> where T : ControllerBase
    {
        protected readonly ApplicationContext Db;
        protected readonly DbContextOptions<ApplicationContext> Options;
        
        protected readonly ILogger<T> Logger;
        protected readonly Provider Provider;

        protected Merchant _merchant;
        protected Event _concert;
        protected List<Ticket> _tickets;
        protected Payment _payment;

        public ControllersBaseTest(String databaseName)
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

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
