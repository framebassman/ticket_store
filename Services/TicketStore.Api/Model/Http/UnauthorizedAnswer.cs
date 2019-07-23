using System;

namespace TicketStore.Api.Model.Http
{
    public class UnauthorizedAnswer : Answer
    {
        public UnauthorizedAnswer()
            : base("unauthorized") { }
    }
}
