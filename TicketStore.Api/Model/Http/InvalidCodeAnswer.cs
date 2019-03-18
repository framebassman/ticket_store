using System;

namespace TicketStore.Api.Model.Http
{
    public class InvalidCodeAnswer : Answer
    {
        public InvalidCodeAnswer()
            : base("cannot find code in database") { }
    }
}
