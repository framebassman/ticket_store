using System;
using TicketStore.Api.Model.Pdf.Model.BarcodeConverters;

namespace TicketStore.Api.Tests.Unit.ModelTests.Preview.Model
{
    public class UnitTestConverter : Converter
    {
        public UnitTestConverter() : base(null)
        {
        }
        
        public override string ToBase64(String origin)
        {
            return "base64";
        }
    }
}
