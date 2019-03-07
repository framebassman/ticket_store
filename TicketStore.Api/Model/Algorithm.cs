using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace TicketStore.Api.Model
{
    public class Algorithm
    {
        private List<Ticket> _tickets;
        public Algorithm(List<Ticket> tickets)
        {
            _tickets = tickets;
        }

        public String Next()
        {
            return Guid.NewGuid().ToString();
        }

        private Int64 bobJenkinsAlgorith(Int32 prev)
        {
            Int64 tmp = prev == 0 ? 0 : prev;
            tmp = tmp - (tmp << 6);
            tmp = tmp ^ ((tmp>>17) & 32767);
            tmp = tmp - (tmp<<9);
            tmp = tmp ^ (tmp<<4);
            tmp = tmp - (tmp<<3);
            tmp = tmp ^ (tmp<<10);
            tmp = tmp ^ ((tmp>>15) & 131071);
            if (tmp < 0)
            {
                return tmp + 4294967296;
            }
            return tmp;
        }
    }
}
