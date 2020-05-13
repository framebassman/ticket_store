using System;
using RestSharp;
using TicketStore.Api.Tests.Environment;

namespace TicketStore.Api.Tests.Model.Services
{
    public abstract class TicketStoreService
    {
        private readonly AppHost _applicationHost;
        protected RestClient Client;
        protected abstract int Port();

        private String Host()
        {
            if (_applicationHost.InsideDockerContainer())
            {
                return DockerContainerName();
            }
            else
            {
                return _applicationHost.Value();
            }
        }
        protected abstract String DockerContainerName();

        public TicketStoreService()
        {
            _applicationHost = new AppHost();
            Client = new RestClient(
                new UriBuilder(
                    "http",
                    Host(),
                    Port()
                ).Uri
            );
        }
    }
}
