using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TicketStore.Data
{
    public class ConnectionString
    {
        private readonly String _origin;
        private readonly Host _host;

        public ConnectionString(String origin, Host host)
        {
            _origin = origin;
            _host = host;
        }
        
        public string Value()
        {
            return ConvertToAdoNet(
                _origin
                    .Replace("$DOCKER_HOST", _host.Value())
                    .Replace("$DATABASE_URL", DatabaseUrlFromEnvironment())
            );
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

        private String DatabaseUrlFromEnvironment()
        {
            return Environment.GetEnvironmentVariable("DATABASE_URL");
        }
    }
}