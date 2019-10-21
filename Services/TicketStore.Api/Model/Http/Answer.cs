using System;

namespace TicketStore.Api.Model.Http
{
    public class Answer
    {
        public String message { get; set; }

        public Answer(String msg)
        {
            message = msg;
        }
    }
}
