using System;
using System.Text.RegularExpressions;

namespace TicketStore.Data.Parsers
{
    public class EnvironmentVariablesParser : AbstractParser
    {
        private readonly Regex _regex;
        
        public EnvironmentVariablesParser(string origin) : base(origin)
        {
            _regex = new Regex("(?<=\\$)(.*?)(?=\\$)");
        }

        public override Boolean ShouldTransform()
        {
            return _regex.IsMatch(Origin);
        }

        public override string Transform()
        {
            var candidate = Origin;
            Match match = _regex.Match(candidate);
            while (match.Success)
            {
                candidate = candidate.Replace(
                    $"${match.Value}$",
                    Environment.GetEnvironmentVariable(match.Value.Replace("$", ""))
                );
                match = _regex.Match(candidate);
            }

            return candidate;
        }
    }
}