using System;
using System.Globalization;
using TicketStore.Data.Model;

namespace TicketStore.Api.Model
{
    public class LabelCalculator
    {
        private Event _concert;
        private String _longDash = "—";

        public LabelCalculator(Event concert)
        {
            _concert = concert;
        }

        public String Value()
        {
            var artist = _concert.Artist;
            var time = _concert.Time;
            return $"{artist} ${_longDash} {time.ToString("d MMMM yyyy", CultureInfo.CreateSpecificCulture("ru-RU"))}";
        }
    }
}