using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using TicketStore.Data;

namespace TicketStore.Web.Tests.Unit.ControllersTests
{
    public abstract class ControllersBaseTest<T> where T : ControllerBase
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