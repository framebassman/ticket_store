using System;

namespace TicketStore.Api.Tests.Model
{
    public class AppHost
    {
        public String Value()
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