using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace TicketStore.Data.Parsers
{
    public class EnvironmentVariablesParser : AbstractParser
    {
        private readonly Regex _regex;
        protected readonly IDictionary EnvVariables;
        
        public EnvironmentVariablesParser(string origin, IDictionary envVariables) : base(origin)
        {
            _regex = new Regex("(?<=\\$)(.*?)(?=\\$)");
            EnvVariables = envVariables;
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
                var envVariable = EnvVariables[match.Value.Replace("$", "")];
                if (envVariable != null)
                {
                    candidate = candidate.Replace(
                        $"${match.Value}$",
                        envVariable.ToString()
                    );                    
                }
                match = _regex.Match(candidate);
            }

            return candidate;
        }
    }
}