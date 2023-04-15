using System;

namespace TicketStore.Api.Model
{
    public interface IGuidProvider
    {
        String NewGuid();
    }
}
