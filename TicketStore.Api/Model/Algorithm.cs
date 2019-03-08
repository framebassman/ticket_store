using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace TicketStore.Api.Model
{
    public class Algorithm
    {
        private List<Ticket> _tickets;
        private Random _random;
        public Algorithm(List<Ticket> tickets)
        {
            _tickets = tickets;
            _random = new Random();
        }

        public String Next()
        {
            return _random.Next(10000000).ToString();
        }
    }
}
