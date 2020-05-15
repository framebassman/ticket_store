using System;
using TicketStore.Data.Parsers;
using Xunit;

namespace TicketStore.Data.Tests.Unit
{
    public class EnvironmentVariableParsersTests
    {
        [Fact]
        public void Test1()
        {
            Environment.SetEnvironmentVariable("var1", "value1", EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("var2", "value2", EnvironmentVariableTarget.Process);
            var parser = new EnvironmentVariablesParser("Host=$var1$;Port=$var2$;Other");
            Assert.Equal("Host=value1;Port=value2;Other", parser.Parse());
        }
    }
}