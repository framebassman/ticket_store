using System;

namespace TicketStore.Web.Model
{
    public class Event
    {
        public Int32 MerchantId { get; set; }
        public String Artist { get; set; }
        public Decimal Roubles { get; set; }
        public String PressRelease { get; set; }
        public DateTime Time { get; set; }
        public Uri PosterUrl { get; set; }
    }
}
