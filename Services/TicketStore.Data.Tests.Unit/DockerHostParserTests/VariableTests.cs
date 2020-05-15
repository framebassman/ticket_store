using System.Collections;
using System.Collections.Generic;
using TicketStore.Data.Parsers;
using Xunit;

namespace TicketStore.Data.Tests.Unit.DockerHostParserTests
{
    [Collection("DockerHostParsersTests")]
    public class VariableTests
    {
        [Fact]
        public void DockerHostWasSetup_ShouldChangeToValue()
        {
            IDictionary environmentVariables = new Dictionary<string, string>();
            environmentVariables.Add("DOCKER_HOST", "tcp://192.168.0.1");
            var parser = new DockerHostParser("Host=$DOCKER_HOST$;Port=5432", environmentVariables);
            Assert.Equal("Host=192.168.0.1;Port=5432", parser.Parse());
        }
    }
}
