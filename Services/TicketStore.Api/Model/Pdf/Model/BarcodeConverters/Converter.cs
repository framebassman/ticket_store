using System;

namespace TicketStore.Api.Model.Pdf.Model.BarcodeConverters
{
    public abstract class Converter
    {
        public abstract String ToBase64(String origin);
    }
}