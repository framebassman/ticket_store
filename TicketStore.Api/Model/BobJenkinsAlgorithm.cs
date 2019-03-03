using System;
using System.Linq;
using System.Collections.Generic;

namespace TicketStore.Api.Model
{
    public class BobJenkinsAlgorithm
    {
        private List<Ticket> _tickets;
        public BobJenkinsAlgorithm(List<Ticket> tickets)
        {
            _tickets = tickets;
        }

        public String Next()
        {
            if (_tickets.Count == 0)
            {
                return "1";
            }
            else
            {
                return (Convert.ToInt32(_tickets.Last().Number) + 1).ToString();
            }
        }
    }
}