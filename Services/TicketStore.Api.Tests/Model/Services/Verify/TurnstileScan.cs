using System;

namespace TicketStore.Api.Tests.Model.Services.Verify
{
    public class TurnstileScan
    {
        public String code;
        public String method;

        public TurnstileScan(String barcode)
        {
            code = barcode;
        }
    }
}
