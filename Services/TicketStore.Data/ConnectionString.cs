using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TicketStore.Data.Parsers;

namespace TicketStore.Data
{
    public class ConnectionString
    {
        private readonly String _origin;
        private readonly AbstractParser _parser;

        public ConnectionString(String origin)
        {
            _origin = origin;
            _parser = new ParsersCascade(origin);
        }
        
        public string Value()
        {
            return _parser.Parse();
        }

        private String ConvertToAdoNet(String jdbc)
        {
            Regex normalRegex = new Regex(
                "Host=.*;Port=.*;Database=.*;Username=.*;Password=.*",
                RegexOptions.IgnoreCase
            ); 
            if (normalRegex.IsMatch(jdbc))
            {
                return jdbc;
            }
            List<String> container = new List<String>
            {
                "Type", "Username", "Password", "Host", "Port", "Database" 
            };

            Regex regex = new Regex("[^:\\/@]+");
            Match match = regex.Match(jdbc);
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