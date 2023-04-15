using System;
using Microsoft.Extensions.Logging;
using TicketStore.Data;
using TicketStore.Data.Model;

namespace TicketStore.Api.Model.Validation
{
    public class TicketFinder : ITicketFinder
    {
        private ApplicationContext _db;
        private ILogger<ITicketFinder> _log;
        private ITicketFinder _manualTicketFinder;
        private ITicketFinder _barcodeTicketFinder;

        public TicketFinder(ApplicationContext context, ILogger<ITicketFinder> log, IDateTimeProvider dateTimeProvider)
        {
            _db = context;
            _log = log;
            _manualTicketFinder = new ManualTicketFinder(context);
            _barcodeTicketFinder = new BarcodeTicketFinder(context, dateTimeProvider);
        }
        public Ticket Find(TurnstileScan barcode)
        {
            _log.LogInformation("Find ticket using verification method: {0}", barcode.method);
            if (barcode.method == VerificationMethod.Barcode) {
                return _barcodeTicketFinder.Find(barcode);
            }

            if (barcode.method == VerificationMethod.Manual) {
                return _manualTicketFinder.Find(barcode);
            }

            throw new Exception($"Verification method doesn't exist: {barcode.method}");
        }
    }
}
