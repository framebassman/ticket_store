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

        public String Layout(String ticketNumber)
        {
            var barcode = Barcode(ticketNumber);
            var layoutPath = Path.Combine("Model", "Pdf", "Templates", "Layout.html");
            var ticketPath = Path.Combine("Model", "Pdf", "Templates", "Ticket.html");
            var barcodePath = Path.Combine("Model", "Pdf", "Templates", "Barcode.html");
            var layoutContent = String.Empty;
            var ticketsContent = String.Empty;
            var barcodesContent = String.Empty;
            using (var barcodesReader = new StreamReader(barcodePath))
            {
                barcodesContent = barcodesReader.ReadToEnd();
                barcodesContent = barcodesContent.Replace("%PICTURE%", barcode);
                using (var ticketsReader = new StreamReader(ticketPath))
                {
                    ticketsContent = ticketsReader.ReadToEnd();
                    ticketsContent = ticketsContent.Replace("%BARCODES%", barcodesContent);
                    using (var layoutReader = new StreamReader(layoutPath))
                    {
                        layoutContent = layoutReader.ReadToEnd();
                        layoutContent = layoutContent.Replace("%TICKET%", ticketsContent);
                        return layoutContent;
                    }                    
                }
            }
        }

        protected abstract String Barcode(String ticketNumber);
    }
}