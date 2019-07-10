using System;

namespace TicketStore.Api.Tests.Environment
{
    public class AppHost
    {
        public String Value()
        {
            return "82.202.236.180";
            var variable = System.Environment.GetEnvironmentVariable("DOCKER_HOST");
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