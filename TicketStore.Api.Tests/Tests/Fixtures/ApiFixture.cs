using System;
using TicketStore.Api.Tests.Model;
using TicketStore.Api.Tests.Data;

namespace TicketStore.Api.Tests.Tests.Fixtures
{
    public class ApiFixture : IDisposable
    {
        public readonly ApiService Api;
        public readonly ApplicationContext Db;

        public ApiFixture()
        {
            Api = new ApiService();
            Db = new ApplicationContext();
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
