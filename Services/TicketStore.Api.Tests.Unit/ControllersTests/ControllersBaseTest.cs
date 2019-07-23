using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using TicketStore.Api.Tests.Unit.TestData;
using TicketStore.Data;

namespace TicketStore.Api.Tests.Unit.ControllersTests
{
    public abstract class ControllersBaseTest<T> where T : ControllerBase
    {
        protected readonly DbContextOptions<ApplicationContext> Options;
        protected readonly ILogger<T> Logger;
        protected readonly Provider Provider;

        public ControllersBaseTest(String databaseName)
        {
            Options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;
            Logger = new Mock<ILogger<T>>().Object;
            Provider = new Provider();
        }
    }
}