using System.Collections;
using System.Collections.Generic;
using TicketStore.Data.Parsers;
using Xunit;

namespace TicketStore.Data.Tests.Unit
{
    public class EnvironmentVariableParsersTests
    {
        [Fact]
        public void Test1()
        {
            IDictionary environmentVariables = new Dictionary<string, string>();
            environmentVariables.Add("var1", "value1");
            environmentVariables.Add("var2", "value2");
            var parser = new EnvironmentVariablesParser("Host=$var1$;Port=$var2$;Other", environmentVariables);
            Assert.Equal("Host=value1;Port=value2;Other", parser.Parse());
        }
    }
}