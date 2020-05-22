using System;
using System.Collections.Generic;
using System.Linq;
using TicketStore.Data;
using TicketStore.Data.Model;

namespace TicketStore.Web.Model.Events
{
    public class EventsFinder
    {
        private ApplicationContext _db;
        private Int32 _merchantId;
        private IDateTimeProvider _dateTime;

        public EventsFinder(ApplicationContext context, Int32 merchantId, IDateTimeProvider dateTime)
        {
            _db = context;
            _merchantId = merchantId;
            _dateTime = dateTime;
        }
        
        public List<Event> Find(Int32 page, Int32 size)
        {
            return _db.Events
                .Where(e => e.MerchantId == _merchantId && e.Time - _dateTime.Now >= TimeSpan.FromHours(6))
                .OrderBy(e => e.Time).Skip(page * size).Take(size).ToList();
        }
    }
}
