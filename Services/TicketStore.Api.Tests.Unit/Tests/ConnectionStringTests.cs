using System;
using TicketStore.Data;
using Xunit;

namespace TicketStore.Api.Tests.Unit.Tests
{
    public class ConnectionStringTests : IDisposable
    {
        private static String _herokuKey = "DATABASE_URL";
        private readonly String _previousDatabaseUrl = Environment.GetEnvironmentVariable(_herokuKey);
        private readonly String _previousDockerHost = Environment.GetEnvironmentVariable("DOCKER_HOST");

        public void Dispose()
        {
            Environment.SetEnvironmentVariable(_herokuKey, _previousDatabaseUrl);
            Environment.SetEnvironmentVariable("DOCKER_HOST", _previousDockerHost);
        }
        
        [Fact]
        public void ConnectionStringAsJdbc_ShouldNormalize()
        {
            Environment.SetEnvironmentVariable(_herokuKey, "postgres://login:password@host:5432/database");
            ConnectionString connectionString = new ConnectionString($"${_herokuKey}", new Host());
            
            Assert.Equal("Host=host;Port=5432;Database=database;Username=login;Password=password", connectionString.Value());
        }

        [Fact]
        public void ReplaceHostAsVariable_ShouldNormalize()
        {
            Environment.SetEnvironmentVariable("DOCKER_HOST", "http://dockerhost");
            ConnectionString connectionString = new ConnectionString("Host=$DOCKER_HOST;Port=5432;Database=database;Username=username;Password=password", new Host());
            
            Assert.Equal("Host=dockerhost;Port=5432;Database=database;Username=username;Password=password", connectionString.Value());
        }
    }
}