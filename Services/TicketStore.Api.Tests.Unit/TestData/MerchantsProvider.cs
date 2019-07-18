using TicketStore.Data.Model;

namespace TicketStore.Api.Tests.Unit.TestData
{
    public class MerchantsProvider
    {
        public Merchant First()
        {
            return new Merchant{ YandexMoneyAccount = "1", Place = "Test" };
        }
    }
}