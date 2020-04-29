using System;

namespace TicketStore.Data.Parsers
{
    public class DockerHostParser : EnvironmentVariablesParser
    {
        public DockerHostParser(string origin) : base(origin)
        {
            Environment.SetEnvironmentVariable(
                "DOCKER_HOST",
                new Host().Value(),
                EnvironmentVariableTarget.Process
            );
        }
    }
}