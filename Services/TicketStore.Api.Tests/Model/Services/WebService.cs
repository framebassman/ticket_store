using RestSharp;

namespace TicketStore.Api.Tests.Model.Services;

public class WebService : TicketStoreService
{
    protected override int Port()
    {
        return 80;
    }

    public RestResponse GetEvents(int merchantId)
    {
        var request = new RestRequest("api/events");
        request.AddParameter("merchantId", merchantId);
        return Client.Execute(request);
    }
}
