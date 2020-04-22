using System.Net.Http;

namespace TicketStore.Api.Tests.Unit.Stubs.Http
{
    public class DummyHttpClientFactory : IHttpClientFactory
    {
        public HttpClient CreateClient(string name)
        {
            return new DummyHttpClient();
        }
    }
}