using TicketStore.Data.Model;

namespace TicketStore.Api.Tests.Unit.TestData
{
    public class MerchantsProvider
    {
        public Merchant First()
        {
            return new Merchant{ Id = 1, YandexMoneyAccount = "1", Place = "Test" };
        }
    }
}