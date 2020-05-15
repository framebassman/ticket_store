using System.Collections;
using System.Collections.Generic;
using TicketStore.Data.Parsers;
using Xunit;

namespace TicketStore.Data.Tests.Unit
{
    public class ParsersCascadeTests
    {
        [Fact]
        public void ShouldReplaceDockerHost()
        {
            IDictionary environmentVariables = new Dictionary<string, string>();
            environmentVariables.Add("DOCKER_HOST", "tcp://192.168.0.1");
            var parser = new ParsersCascade("Host=$DOCKER_HOST$;Port=5432", environmentVariables);
            Assert.Equal("Host=192.168.0.1;Port=5432", parser.Parse());
        }

        [Fact]
        public void ShouldReplaceDatabaseUrl()
        {
            IDictionary environmentVariables = new Dictionary<string, string>();
            environmentVariables.Add("DATABASE_URL", "postgres://login:password@host:5432/database");
            var parser = new ParsersCascade("$DATABASE_URL$", environmentVariables);
            Assert.Equal("Host=host;Port=5432;Database=database;Username=login;Password=password;SSL Mode=Require;Trust Server Certificate=true", parser.Parse());
        }
    }
}