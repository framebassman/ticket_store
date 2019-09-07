using System;
using TicketStore.Api.Model.Validation;

namespace TicketStore.Api.Tests.Unit.Model
{
    public class UnknownTurnstileScan : TurnstileScan
    {
        public UnknownTurnstileScan(String barcode)
        {
            method = "Unknown";
            code = barcode;
        }
    }
}
