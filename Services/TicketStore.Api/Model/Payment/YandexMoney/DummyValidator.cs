using System;
using Microsoft.Extensions.Logging;

namespace TicketStore.Api.Model.Payment.YandexMoney
{
  public class DummyValidator : IPaymentValidator
    {
        private ILogger<Validator> _log;

        public DummyValidator(ILogger<Validator> log)
        {
            _log = log;
        }

        public Boolean FromYandex(String notification_type, String operation_id, Decimal amount,
            String currency, DateTime datetime, String sender, Boolean codepro, String notification_secret,
            String label, String sha1Hash)
        {
            _log.LogInformation("Dummy Validator should answer {0} always", true);
            return true;
        }
    }
}
