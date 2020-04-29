using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TicketStore.Data.Parsers
{
    public class JdbcParser : AbstractParser
    {
        public JdbcParser(string origin) : base(origin)
        {
        }

        public override bool ShouldTransform()
        {
            return !_shouldNotTransform();
        }

        private bool _shouldNotTransform()
        {
            return Origin.Contains("Host=")
                   || Origin.Contains("Port=")
                   || Origin.Contains("Database=")
                   || Origin.Contains("Username=")
                   || Origin.Contains("Password=");
        }

        public override string Transform()
        {
            List<String> container = new List<String>
            {
                "Type", "Username", "Password", "Host", "Port", "Database" 
            };

            Regex regex = new Regex("[^:\\/@]+");
            Match match = regex.Match(Origin);
            int i = 0;
            while (match.Success)
            {
                Group group = match.Groups[0];
                container[i] = group.Value;
                match = match.NextMatch();
                i++;
            }
            String username = container[1];
            String password = container[2];
            String host = container[3];
            String port = container[4];
            String database = container[5];
            return $"Host={host};Port={port};Database={database};Username={username};Password={password}";
        }
    }
}