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

        public IRestResponse<List<Email>> EmailsForAddress(string to)
        {
            var request = new RestRequest($"api/emails/{to}", Method.GET);
            request.AddHeader("Content-Type", "application/json");
            return Client.Execute<List<Email>>(request);
        }
    }
}
