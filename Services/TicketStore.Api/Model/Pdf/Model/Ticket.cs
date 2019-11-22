using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace TicketStore.Api.Model.Pdf.Model
{
    public class Ticket : TemplateModel
    {
        private List<Barcode> _barcodes;
        private String _artist;
        private DateTime _time;
        private Decimal _price;
        private CultureInfo _culture;
        
        public Ticket(List<Barcode> barcodes, String artist, DateTime time, Decimal price)
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
            template = template.Replace("%TIME%", FormatTime(_time));
            template = template.Replace("%PRICE%", _price.ToString(_culture));
            return template;
        }

        protected override string PathToTemplate()
        {
            return Path.Combine("Model", "Pdf", "Templates", "Ticket.html");
        }
        
        private String FormatTime(DateTime origin)
        {
            var mskTimezone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Moscow");
            var originTime = DateTime.SpecifyKind(origin, DateTimeKind.Utc);
            var withTimezone = TimeZoneInfo.ConvertTimeFromUtc(originTime, mskTimezone);
            var allLowerCase = withTimezone.ToString("f", _culture);
            var firstLetterIsCapital = new StringBuilder()
                .Append(allLowerCase[0].ToString().ToUpper())
                .Append(allLowerCase.Substring(1, allLowerCase.Length - 1))
                .ToString();
            return firstLetterIsCapital;
        }
    }
}