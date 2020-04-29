using System;
using TicketStore.Data.Parsers;
using Xunit;

namespace TicketStore.Data.Tests.Unit
{
    public class ParsersCascadeTests
    {
        [Fact]
        public void ShouldReplaceDockerHost()
        {
            Environment.SetEnvironmentVariable("DOCKER_HOST", "tcp://192.168.0.1", EnvironmentVariableTarget.Process);
            var parser = new ParsersCascade("Host=$DOCKER_HOST$;Port=5432");
            Assert.Equal("Host=192.168.0.1;Port=5432", parser.Parse());
        }

        [Fact]
        public void ShouldReplaceDatabaseUrl()
        {
            Environment.SetEnvironmentVariable("DATABASE_URL", "postgres://login:password@host:5432/database", EnvironmentVariableTarget.Process);
            var parser = new ParsersCascade("$DATABASE_URL$");
            Assert.Equal("Host=host;Port=5432;Database=database;Username=login;Password=password", parser.Parse());
        }
    }
}