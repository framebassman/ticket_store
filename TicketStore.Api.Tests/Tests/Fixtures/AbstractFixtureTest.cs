using Xunit;

namespace TicketStore.Api.Tests.Tests.Fixtures
{
    public class AbstractFixtureTest : IClassFixture<ApiFixture>
    {
        protected ApiFixture Fixture;

        public AbstractFixtureTest(ApiFixture fixture)
        {
            this.Fixture = fixture;
        }
    }
}
