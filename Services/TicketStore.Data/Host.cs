using System;

namespace TicketStore.Data
{
    public class Host
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