using System;
using System.Globalization;
using TicketStore.Api.Tests.Model.Db;

namespace TicketStore.Api.Tests.Model
{
    public class LabelCalculator
    {
        private Event _concert;

        public LabelCalculator(Event concert)
        {
            _concert = concert;
        }

        public String Value()
        {
            var artist = _concert.Artist;
            var time = _concert.Time;
            return $"{artist} — {time.ToString("d MMMM yyyy", CultureInfo.CreateSpecificCulture("ru-RU"))}";
        }
    }
}