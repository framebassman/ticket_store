using TicketStore.Data.Parsers;
using Xunit;

namespace TicketStore.Data.Tests.Unit
{
    public class JdbcParserTests
    {
        [Fact]
        public void Test()
        {
            var parser = new JdbcParser("postgres://login:password@host:5432/database");
            Assert.Equal("Host=host;Port=5432;Database=database;Username=login;Password=password", parser.Parse());
        }
    }
}