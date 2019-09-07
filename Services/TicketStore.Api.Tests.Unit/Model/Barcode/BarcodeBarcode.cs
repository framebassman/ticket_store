using System;
using TicketStore.Api.Model.Validation;

namespace TicketStore.Api.Tests.Unit.Model
{
    public class BarcodeBarcode : Barcode
    {
        public BarcodeBarcode(String co)
        {
            method = "Barcode";
            code = co;
        }
    }
}
