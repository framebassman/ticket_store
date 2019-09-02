using System;

namespace TicketStore.Api.Model.Http
{
    public class VerifiedAnswer
    {
        public String message;
        public String concertLabel;
        public Boolean used;

        public VerifiedAnswer(string concLabel)
        {
            message = "OK";
            concertLabel = concLabel;
        }
    }
}
