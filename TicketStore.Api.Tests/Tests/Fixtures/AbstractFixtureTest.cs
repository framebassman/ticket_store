using System.Collections.Generic;
using TicketStore.Api.Tests.Model.Db;
using Xunit;

namespace TicketStore.Api.Tests.Tests.Fixtures
{
    public class AbstractFixtureTest : IClassFixture<ApiFixture>
    {
        protected ApiFixture Fixture;
        protected Merchant Merchant;
        protected List<Event> Events;
        

        public AbstractFixtureTest(ApiFixture fixture)
        {
            this.Fixture = fixture;
            this.Merchant = fixture.Merchant;
            this.Events = fixture.Events;
        }
    }
}
