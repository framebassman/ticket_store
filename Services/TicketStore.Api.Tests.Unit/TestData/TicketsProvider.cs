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
                new Ticket
                {
                    CreatedAt = new DateTime(2019, 7, 9, 16, 10, 0, DateTimeKind.Utc),
                    Number = "1111122222",
                    Expired = false,
                    Roubles = 100,
                    Event = _concert,
                    EventId = _concert.Id,
                },
                new Ticket
                {
                    CreatedAt = new DateTime(2019, 7, 9, 16, 10, 0, DateTimeKind.Utc),
                    Number = "3333344444",
                    Expired = true,
                    Roubles = 100,
                    Event = _concert,
                    EventId = _concert.Id,
                },
                new Ticket
                {
                    CreatedAt = new DateTime(2019, 7, 9, 16, 10, 0, DateTimeKind.Utc),
                    Number = "5555566666",
                    Expired = false,
                    Roubles = 100,
                }
            };

            return tickets;
        }
    }
}