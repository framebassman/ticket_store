using System;

namespace TicketStore.Api.Model.Http
{
    public class ConcertPassedAnswer : Answer
    {
        public ConcertPassedAnswer()
            : base("concert has already passed") { }
    }
}
