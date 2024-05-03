using System;
using RestSharp;

namespace TicketStore.Api.Tests.Model.Services
{
    public abstract class TicketStoreService
    {
        protected RestClient Client;
        protected abstract int Port();

        public TicketStoreService()
        {
            Client = new RestClient(
                new UriBuilder(
                    "http",
                    "localhost",
                    Port()
                ).Uri
            );
        }
    }
}
