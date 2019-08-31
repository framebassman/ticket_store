using System;
using System.Collections.Generic;
using TicketStore.Data.Model;

namespace TicketStore.Api.Tests.Unit.TestData
{
    public class TicketsProvider
    {
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
                },
                new Ticket
                {
                    CreatedAt = new DateTime(2019, 7, 9, 16, 10, 0, DateTimeKind.Utc),
                    Number = "3333344444",
                    Expired = true,
                    Roubles = 100,
                }
            };

            return tickets;
        }
    }
}