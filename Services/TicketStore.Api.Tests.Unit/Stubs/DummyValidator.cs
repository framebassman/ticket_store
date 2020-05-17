using System;
using TicketStore.Api.Model;
using Microsoft.Extensions.Logging;

namespace TicketStore.Api.Tests.Unit.Stubs
{
    public class DummyValidator : Validator
    {
        public DummyValidator(ILogger<Validator> log) : base(log)
        {
        }

        public override Boolean FromYandex(
            String notification_type,
            String operation_id,
            Decimal amount,
            String currency,
            DateTime datetime,
            String sender,
            Boolean codepro,
            String notification_secret,
            String label,
            String sha1Hash
        )
        {
            return true;
        }
    }
}