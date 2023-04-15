using System;
using System.Collections.Generic;
using System.Net.Http;
using DinkToPdf;
using DinkToPdf.Contracts;
using TicketStore.Api.Model.PdfDocument.Model.BarcodeConverters;
using TicketStore.Data.Model;

namespace TicketStore.Api.Model.PdfDocument
{
    public class Pdf
    {
        private IConverter _pdfConverter;
        protected Preview Preview;

        public Pdf(Event concert, List<Ticket> tickets, IConverter pdfConverter, Converter barcodeConverter, HttpClient client)
        {
            _pdfConverter = pdfConverter;
            Preview = new Preview(client, concert, tickets, barcodeConverter);
        }

        public byte[] ToBytes()
        {
            return _pdfConverter.Convert(Template());
        }

        public String ConcertTime()
        {
            return Preview.ConcertTime();
        }

        private HtmlToPdfDocument Template()
        {
            return new HtmlToPdfDocument
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                },
                Objects = {
                    new ObjectSettings {
                        PagesCount = true,
                        HtmlContent = Preview.Layout(),
                        WebSettings = { DefaultEncoding = "utf-8" },
                        HeaderSettings = { FontSize = 9, Right = "[page] страница из [toPage]", Line = true, Spacing = 2.812 }
                    }
                }
            };
        }
    }
}
