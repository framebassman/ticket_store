using System;
using System.Collections.Generic;
using System.Net.Http;
using DinkToPdf;
using DinkToPdf.Contracts;
using TicketStore.Data.Model;

namespace TicketStore.Api.Model.Pdf
{
    public class Pdf
    {
        private readonly IConverter _converter;
        private readonly Preview _preview;

        public Pdf(Event concert, List<Ticket> tickets, IConverter converter, HttpClient client)
        {
            _converter = converter;
            _preview = new Preview(client, concert);
        }

        public byte[] ToBytes()
        {
            return _converter.Convert(Template());
        }

        public String ConcertTime()
        {
            return _preview.ConcertTime();
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
                        HtmlContent = _preview.Layout(),
                        WebSettings = { DefaultEncoding = "utf-8" },
                        HeaderSettings = { FontSize = 9, Right = "[page] страница из [toPage]", Line = true, Spacing = 2.812 }
                    }
                }
            };
        }
    }
}
