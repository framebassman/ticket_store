using System;
using System.Globalization;
using Newtonsoft.Json;
using TicketStore.Api.Model.Http;
using TicketStore.Api.Tests.Model.Db;

namespace TicketStore.Api.Tests.Tests.Matchers.Tickets
{
    public class WithConcert : TicketMatcher
    {
        private Event _concert;
        
        public WithConcert(Event concert)
        {
            _concert = concert;
        }

        public override bool Matches(String json)
        {
            var actual = JsonConvert.DeserializeObject<VerifiedAnswer>(json);
            return actual.concertLabel == CalculateLabel();
        }

        private String CalculateLabel()
        {
            var longDash = "—";
            var artist = _concert.Artist;
            var time = _concert.Time;
            return $"{artist} ${longDash} {time.ToString("d MMMM yyyy", CultureInfo.CreateSpecificCulture("ru-RU"))}";
        }
    }
}