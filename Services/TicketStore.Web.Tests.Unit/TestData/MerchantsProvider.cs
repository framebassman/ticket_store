using TicketStore.Data.Model;

namespace TicketStore.Web.Tests.Unit.TestData
{
    public class MerchantsProvider
    {
        public Merchant First()
        {
            return new Merchant{ YandexMoneyAccount = "1", Place = "Test" };
        }
    }
}
