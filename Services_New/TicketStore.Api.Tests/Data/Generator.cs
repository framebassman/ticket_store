using System;

namespace TicketStore.Api.Tests.Data
{
    public class Generator
    {
        public static String Email()
        {
            return Guid.NewGuid() + "@test.test";
        }

        public static String YandexMoneyAccount()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
