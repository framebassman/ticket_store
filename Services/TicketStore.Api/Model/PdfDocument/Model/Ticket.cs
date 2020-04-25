using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace TicketStore.Api.Model.PdfDocument.Model
{
    public class Ticket : TemplateModel
    {
        private List<Barcode> _barcodes;
        private String _artist;
        private String _time;
        private Decimal _price;
        private CultureInfo _culture;
        
        public Ticket(List<Barcode> barcodes, String artist, String time, Decimal price)
        {
            _barcodes = barcodes;
            _artist = artist;
            _time = time;
            _price = price;
            _culture = CultureInfo.CreateSpecificCulture("ru-RU");
        }

        public override string ToHtml()
        {
            var template = ReadTemplate();
            var barcodesInHtml = String.Empty;
            foreach (var barcode in _barcodes)
            {
                barcodesInHtml += barcode.ToHtml();
            }

            template = template.Replace("%BARCODES%", barcodesInHtml);
            template = template.Replace("%ARTIST%", _artist);
            template = template.Replace("%TIME%", _time);
            template = template.Replace("%PRICE%", _price.ToString(_culture));
            return template;
        }

        protected override string PathToTemplate()
        {
            return Path.Combine("Model", "PdfDocument", "Templates", "Ticket.html");
        }
    }
}