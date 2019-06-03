using TicketStore.Api.Tests.Model;

namespace TicketStore.Api.Tests.Tests.Fixtures
{
    public class ApiFixture
    {
        public ApiService Api;

        public ApiFixture()
        {
            Api = new ApiService();
        }
    }
}
