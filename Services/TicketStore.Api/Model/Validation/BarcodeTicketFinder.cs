using System;
using System.Collections.Generic;
using System.Linq;
using TicketStore.Api.Model.Validation.Exceptions;
using TicketStore.Data;
using TicketStore.Data.Model;

namespace TicketStore.Api.Model.Validation
{
    public class BarcodeTicketFinder : ITicketFinder
    {
        private readonly ApplicationContext _db;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly List<ITicketFinder> _barcodeFinders;
        
        public BarcodeTicketFinder(ApplicationContext context, IDateTimeProvider dateTimeProvider)
        {
            _db = context;
            _dateTimeProvider = dateTimeProvider;
            _barcodeFinders = new List<ITicketFinder>
            {
                new ManualTicketFinder(context),
                new BarcodeNetFinder(context)
            };
        }
        public Ticket Find(TurnstileScan barcode)
        {
            Ticket ticket = null;
            foreach (var finder in _barcodeFinders)
            {
                try
                {
                    ticket = finder.Find(barcode);
                }
                catch (FindException e)
                {
                    if (finder.Equals(_barcodeFinders.Last()))
                    {
                        throw e;
                    }
                }
            }

            if (ticket == null)
            {
                throw new TicketNotFound(VerificationMethod.Barcode);
            }
            else
            {
                return ticket;
            }

//            var concert = _db.Events.FirstOrDefault(e => e.Id == ticket.EventId);
//            if (concert == null)
//            {
//                throw new ConcertNotFound(VerificationMethod.Barcode);
//            };

            // TimeSpan dateDiff = _dateTimeProvider.Now - concert.Time;
            // var hoursDiff = Math.Abs(dateDiff.TotalHours);
            // if (dateDiff.TotalHours <= -12) {
            //     throw new TooEarly(VerificationMethod.Barcode, hoursDiff);
            // }

            // if (dateDiff.TotalHours >= 12) {
            //     throw new TooLate(VerificationMethod.Barcode, hoursDiff);
            // }
        }
    }
}
