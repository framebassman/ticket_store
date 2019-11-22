using System.Net.Http;

namespace TicketStore.Api.Model.Pdf
{
    public class FakePreview : Preview
    {
        public FakePreview(HttpClient client) : base(client)
        {
        }
    }
}