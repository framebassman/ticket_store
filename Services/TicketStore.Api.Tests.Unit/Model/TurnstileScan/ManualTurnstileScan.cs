using System;
using TicketStore.Api.Model.Validation;

namespace TicketStore.Api.Tests.Unit.Model
{
    public class ManualTurnstileScan : TurnstileScan
    {
        public ManualTurnstileScan(String barcode)
        {
            method = "Manual";
            code = barcode;
        }
    }
}
