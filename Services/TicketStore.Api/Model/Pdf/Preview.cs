using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace TicketStore.Api.Model.Pdf
{
    public abstract class Preview
    {
        protected readonly HttpClient Client;

        protected Preview(HttpClient client)
        {
            Client = client;
        }

        protected abstract String Barcode(String ticketNumber);

        public String Layout(String ticketNumber)
        {
            var ticketNumbers = new List<String>();
            ticketNumbers.Add(ticketNumber);
            ticketNumbers.Add(ticketNumber);
            var layoutPath = Path.Combine("Model", "Pdf", "Templates", "Layout.html");
            var ticketPath = Path.Combine("Model", "Pdf", "Templates", "Ticket.html");
            var barcodePath = Path.Combine("Model", "Pdf", "Templates", "Barcode.html");
            var layoutTemplate = ReadTemplate(layoutPath);
            var ticketTemplate = ReadTemplate(ticketPath);
            var barcodeTemplate = ReadTemplate(barcodePath);

            var barcodesPreview = String.Empty;
            foreach (var number in ticketNumbers)
            {
                var replaced = barcodeTemplate.Replace("%PICTURE%", Barcode(number));
                barcodesPreview += replaced;
            }

            var ticketPreview = ticketTemplate.Replace("%BARCODES%", barcodesPreview);

            var layoutPreview = layoutTemplate.Replace("%TICKET%", ticketPreview);
            return layoutPreview;
        }

        private String ReadTemplate(string path)
        {
            using (var reader = new StreamReader(path))
            {
                return reader.ReadToEnd();
            }
        }
    }
}