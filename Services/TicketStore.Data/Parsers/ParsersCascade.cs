using System;
using System.Collections.Generic;
using System.Linq;

namespace TicketStore.Data.Parsers
{
    public class ParsersCascade : AbstractParser
    {
        private List<AbstractParser> _parsers;
        
        public ParsersCascade(String origin) : base(origin)
        {
            _parsers = new List<AbstractParser>();
            _parsers.Add(new DockerHostParser(origin));
            _parsers.Add(new EnvironmentVariablesParser(origin));
            _parsers.Add(new HerokuParser(origin));
        }
        
        public override string Transform()
        {
            foreach (var parser in _parsers)
            {
                Origin = parser.Transform();
            }

            return Origin;
        }

        public override Boolean ShouldTransform()
        {
            var modifiedParsers = new List<AbstractParser>();
            foreach (var parser in _parsers)
            {
                if (parser.ShouldTransform())
                {
                    modifiedParsers.Add(parser);
                }
            }

            _parsers = modifiedParsers;
            return _parsers.Any();
        }
    }
}