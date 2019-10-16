using System;
using System.Text;

namespace TicketStore.Api.Model
{
    public class Validator
    {
        private String _params;

        public Validator(
            String notification_type,
            String operation_id,
            Decimal amount,
            String currency,
            DateTime datetime,
            String sender,
            Boolean codepro,
            String notification_secret,
            String label
        )
        {
            _params = new StringBuilder()
                .Append(notification_type).Append("&")
                .Append(operation_id).Append("&")
                .Append(amount).Append("&")
                .Append(amount).Append("&")
                .Append(currency).Append("&")
                .Append(datetime.ToUniversalTime()).Append("&")
                .Append(sender).Append("&")
                .Append(codepro).Append("&")
                .Append(notification_secret).Append("&")
                .Append(label)
                .ToString();
        }

        public Boolean FromYandex()
        {
            // TODO: Implement logic
            return true;
        }
    }
}