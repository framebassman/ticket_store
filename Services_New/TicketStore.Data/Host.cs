using System;
using System.Collections;

namespace TicketStore.Data
{
    public class Host
    {
        private IDictionary _environmentVariables;
        
        public Host(IDictionary environmentVariables)
        {
            _environmentVariables = environmentVariables;
        }
        
        public String Value()
        {
            var variable = _environmentVariables["DOCKER_HOST"];
            if (variable == null)
            {
                return "localhost";
            }
            if (String.IsNullOrEmpty(variable.ToString()))
            {
                return "localhost";
            }

            return new UriBuilder(variable.ToString()).Host;
        }
    }
}
