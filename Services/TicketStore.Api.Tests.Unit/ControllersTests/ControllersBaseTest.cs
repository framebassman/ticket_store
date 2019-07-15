using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using TicketStore.Data;

namespace TicketStore.Api.Tests.Unit.ControllersTests
{
    public class ControllersBaseTest<T>
    {
        protected readonly DbContextOptions<ApplicationContext> Options;
        protected readonly ILogger<T> Logger;

        public ControllersBaseTest(String databaseName)
        {
            Options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;
            Logger = new Mock<ILogger<T>>().Object;
        }
    }
}