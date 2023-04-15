using TicketStore.Data.Parsers;
using Xunit;

namespace TicketStore.Data.Tests.Unit
{
    public class HerokuParserTests
    {
        [Fact]
        public void Test1()
        {
            var parser = new HerokuParser("postgres://login:password@host:5432/database");
            Assert.Equal(
                "Host=host;Port=5432;Database=database;Username=login;Password=password;SSL Mode=Require;Trust Server Certificate=true",
                parser.Parse()
            );
        }
        
        [Fact]
        public void Test2()
        {
            var parser = new HerokuParser("postgres://login:password@host:5432/database;");
            Assert.Equal(
                "Host=host;Port=5432;Database=database;Username=login;Password=password;SSL Mode=Require;Trust Server Certificate=true",
                parser.Parse()
            );
        }
    }
}