using System;
using System.Collections.Generic;
using System.Linq;

namespace TicketStore.Data.Parsers
{
    public class ParsersCascade : AbstractParser
    {
        private List<Func<String, AbstractParser>> _parserCreators;
        
        public ParsersCascade(String origin) : base(origin)
        {
            _parserCreators = new List<Func<String, AbstractParser>>();
            _parserCreators.Add((src) => new DockerHostParser(src));
            _parserCreators.Add((src) => new EnvironmentVariablesParser(src));
            _parserCreators.Add((src) => new HerokuParser(src));
        }
        
        public override string Transform()
        {
            foreach (var creator in _parserCreators)
            {
                Origin = creator.Invoke(Origin).Transform();
            }

            return Origin;
        }

        public override Boolean ShouldTransform()
        {
            var modifiedCreators = new List<Func<String, AbstractParser>>();
            foreach (var creator in _parserCreators)
            {
                if (creator.Invoke(Origin).ShouldTransform())
                {
                    modifiedCreators.Add(creator);
                }
            }

            _parserCreators = modifiedCreators;
            return _parserCreators.Any();
        }
    }
}