using System;
using System.Collections.Generic;

namespace TicketStore.Data.Parsers
{
    public class ParsersCascade : AbstractParser
    {
        private readonly List<AbstractParser> _parsers;
        
        public ParsersCascade(String origin) : base(origin)
        {
            _parsers = new List<AbstractParser>
            {
                new EnvironmentVariablesParser(origin)
            };
        }
        
        public override string Transform()
        {
            var candidate = Origin;
            foreach (var parser in _parsers)
            {
                candidate = parser.Transform();
            }

            return candidate;
        }

        public override Boolean ShouldTransform()
        {
            return true;
        }
    }
}