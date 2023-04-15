using System.Collections;

namespace TicketStore.Data.Parsers
{
    public class DockerHostParser : EnvironmentVariablesParser
    {
        public DockerHostParser(string origin, IDictionary environmentVariables)
            : base(origin, environmentVariables)
        {
            var host = new Host(EnvVariables).Value();
            if (EnvVariables.Contains("DOCKER_HOST"))
            {
                EnvVariables.Remove("DOCKER_HOST");
            }
            EnvVariables.Add("DOCKER_HOST", host);
        }
    }
}
