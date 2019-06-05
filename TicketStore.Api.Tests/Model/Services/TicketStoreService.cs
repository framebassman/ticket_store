using System;
using RestSharp;

namespace TicketStore.Api.Tests.Model.Services
{
    public abstract class TicketStoreService
    {
        protected readonly RestClient Client;
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
