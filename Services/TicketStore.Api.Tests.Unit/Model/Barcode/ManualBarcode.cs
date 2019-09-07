using System;
using TicketStore.Api.Model.Validation;

namespace TicketStore.Api.Tests.Unit.Model
{
    public class ManualBarcode : Barcode
    {
        public ManualBarcode(String co)
        {
            method = "Manual";
            code = co;
        }
    }
}
