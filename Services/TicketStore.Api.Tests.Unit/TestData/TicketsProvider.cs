using System;
using System.Collections.Generic;
using TicketStore.Data.Model;

namespace TicketStore.Api.Tests.Unit.TestData
{
    public class TicketsProvider
    {
        private readonly Event _concert;

        public TicketsProvider(Event concert)
        {
            _concert = concert;
        }
        public List<Ticket> List()
        {
            var tickets = new List<Ticket>
            {
                CreateTicket("1111122222", false),
                CreateTicket("3333344444", true),
                new Ticket
                {
                    CreatedAt = new DateTime(2019, 7, 9, 16, 10, 0, DateTimeKind.Utc),
                    Number = "5555566666",
                    Expired = false,
                    Roubles = 100,
                },
                CreateTicket("7777788", false),
                CreateTicket("777778", false),
            };

            return tickets;
        }

        private Ticket CreateTicket(String number, Boolean expired)
        {
            return new Ticket
            {
                CreatedAt = new DateTime(2019, 7, 9, 16, 10, 0, DateTimeKind.Utc),
                Number = number,
                Expired = expired,
                Roubles = 100,
                Event = _concert,
                EventId = _concert.Id,
            };
        }
    }
}