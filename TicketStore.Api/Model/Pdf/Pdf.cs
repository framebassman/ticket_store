using System;
using System.Collections.Generic;
using DinkToPdf;

namespace TicketStore.Api.Model.Pdf
{
    public class Pdf
    {
        private List<Ticket> _tickets;
        public Pdf(List<Ticket> tickets)
        {
            _tickets = tickets;
        }

        public byte[] toBytes()
        {
            var converter = new SynchronizedConverter(new PdfTools());
            var temp = template();
            return converter.Convert(temp);
        }

        private HtmlToPdfDocument template()
        {
            return new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A6,
                },
                Objects = {
                    new ObjectSettings() {
                        PagesCount = true,
                        HtmlContent = @"
                            <div>
                                <div>Билет номер</div>
                                <img src=""http://windowsarena.ru/wp-content/uploads/2016/04/yandex_logo.png""/>
                            </div>
                        ",
                        WebSettings = { DefaultEncoding = "utf-8" },
                        HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 }
                    }
                }
            };
        }
    }
}
