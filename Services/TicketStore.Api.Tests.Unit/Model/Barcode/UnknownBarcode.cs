using System;
using TicketStore.Api.Model.Validation;

namespace TicketStore.Api.Tests.Unit.Model
{
    public class UnknownBarcode : Barcode
    {
        public UnknownBarcode(String co)
        {
            method = "Unknown";
            code = co;
        }
    }
}
