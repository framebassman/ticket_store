using System;
using TicketStore.Data.Parsers;

namespace TicketStore.Data
{
    public class ConnectionString
    {
        private readonly AbstractParser _parser;

        public ConnectionString(String origin)
        {
            _parser = new ParsersCascade(origin, Environment.GetEnvironmentVariables(EnvironmentVariableTarget.Process));
        }
        
        public string Value()
        {
            return _parser.Parse();
        }
    }
}