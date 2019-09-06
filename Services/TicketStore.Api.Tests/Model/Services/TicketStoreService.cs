using System;
using RestSharp;
using TicketStore.Api.Tests.Environment;

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
                    new AppHost().Value(),
                    this.Port()
                ).Uri
            );
        }
    }
}
