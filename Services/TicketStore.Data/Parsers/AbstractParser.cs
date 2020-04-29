using System;

namespace TicketStore.Data.Parsers
{
    public abstract class AbstractParser
    {
        protected String Origin;

        public AbstractParser(String origin)
        {
            Origin = origin;
        }

        public String Parse()
        {
            if (ShouldTransform())
            {
                return Transform();
            }
            else
            {
                return Origin;
            }
        }

        public abstract Boolean ShouldTransform();
        public abstract String Transform();
    }
}
