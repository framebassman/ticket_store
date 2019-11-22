using System;
using System.IO;
using TicketStore.Api.Model.Pdf.Model.BarcodeConverters;

namespace TicketStore.Api.Model.Pdf.Model
{
    public class Barcode : TemplateModel
    {
        private String _ticketNumber;
        private Converter _converter;
        
        public Barcode(String ticketNumber, Converter converter)
        {
            _ticketNumber = ticketNumber;
            _converter = converter;
        }
        
        protected override string PathToTemplate()
        {
            return Path.Combine("Model", "Pdf", "Templates", "Barcode.html");
        }

        public override string ToHtml()
        {
            var template = ReadTemplate();
            var base64Number = _converter.ToBase64(_ticketNumber);
            return template.Replace("%PICTURE%", base64Number);
        }
    }
}