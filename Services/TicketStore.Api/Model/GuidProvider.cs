using System;

namespace TicketStore.Api.Model
{
    public class GuidProvider : IGuidProvider
    {
        public String NewGuid() => Guid.NewGuid().ToString();
    }
}
