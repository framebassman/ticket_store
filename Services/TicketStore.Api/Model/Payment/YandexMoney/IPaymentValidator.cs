using System;

namespace TicketStore.Api.Model.Payment.YandexMoney
{
    public interface IPaymentValidator
    {
        Boolean FromYandex(String notification_type, String operation_id, Decimal amount,
            String currency, DateTime datetime, String sender, Boolean codepro, String notification_secret,
            String label, String sha1Hash);
    }
}
