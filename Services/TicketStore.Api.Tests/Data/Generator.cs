using System;

namespace TicketStore.Api.Tests.Data
{
    public class Generator
    {
        public static string Email()
        {
            return Guid.NewGuid().ToString() + "@test.test";
        }
    }
}
