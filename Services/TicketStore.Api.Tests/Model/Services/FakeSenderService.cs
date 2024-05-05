using System.Collections.Generic;
using RestSharp;

namespace TicketStore.Api.Tests.Model.Services
{
    public class FakeSenderService : TicketStoreService
    {
        protected override int Port()
        {
            return 5000;
        }

        public RestResponse<List<Email>> EmailsForAddress(string to)
        {
            var request = new RestRequest($"fake_sender/emails/{to}", Method.Get);
            request.AddHeader("Content-Type", "application/json");
            return Client.Execute<List<Email>>(request);
        }
    }
}
