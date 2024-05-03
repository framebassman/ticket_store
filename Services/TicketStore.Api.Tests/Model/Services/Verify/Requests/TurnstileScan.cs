using System;

namespace TicketStore.Api.Tests.Model.Services.Verify.Requests
{
    public abstract class TurnstileScan
    {
        public String code { get; set; }
        public String method { get; set; }

        public TurnstileScan(String barcode)
        {
            code = barcode;
        }
    }
}
