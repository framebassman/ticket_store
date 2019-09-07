using System;
using TicketStore.Api.Model.Validation;

namespace TicketStore.Api.Tests.Unit.Model
{
    public class BarcodeTurnstileScan : TurnstileScan
    {
        public BarcodeTurnstileScan(String barcode)
        {
            method = "Barcode";
            code = barcode;
        }
    }
}
