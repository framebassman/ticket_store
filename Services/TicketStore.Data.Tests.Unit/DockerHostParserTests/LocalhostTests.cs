using System;
using TicketStore.Data.Parsers;
using Xunit;

namespace TicketStore.Data.Tests.Unit.DockerHostParserTests
{
    [Collection("DockerHostParsersTests")]
    public class LocalhostTests
    {
        [Fact]
        public void DockerHostIsEmpty_ShouldChangeToLocalhost()
        {
            Environment.SetEnvironmentVariable("DOCKER_HOST", "", EnvironmentVariableTarget.Process);
            var parser = new DockerHostParser("Host=$DOCKER_HOST$;Port=5432");
            Assert.Equal("Host=localhost;Port=5432", parser.Parse());
        }
    }
}
