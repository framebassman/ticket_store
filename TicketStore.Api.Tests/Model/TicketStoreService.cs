using System;
using RestSharp;

namespace TicketStore.Api.Tests.Model
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
                    this.Host(),
                    this.Port()
                ).Uri
            );
        }

        private String Host()
        {
            var variable = Environment.GetEnvironmentVariable("DOCKER_HOST");
            if (String.IsNullOrEmpty(variable))
            {
                return "localhost";
            }
            else
            {
                return new UriBuilder(variable).Host;
            }
        }
    }
}
