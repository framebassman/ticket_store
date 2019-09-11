using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using TicketStore.Data;
using TicketStore.Web.Tests.Unit.TestData;

namespace TicketStore.Web.Tests.Unit.BaseTest
{
    public abstract class DbBaseTest<T> : IDisposable
    {
        protected readonly DbContextOptions<ApplicationContext> Options;
        protected readonly ILogger<T> Logger;
        protected readonly Provider TestData;
        
        public DbBaseTest(String databaseName)
        {
            Options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;
            Logger = new Mock<ILogger<T>>().Object;
            TestData = new Provider();
        }

        public abstract void Dispose();
    }
}
