using System;
using System.Collections.Generic;
using System.Linq;
using TicketStore.Api.Tests.Data;
using TicketStore.Api.Tests.Model.Db;

namespace TicketStore.Api.Tests.Tests.DevelopmentData
{
    public class DevelopmentData
    {
        private readonly Merchant _merchant;
        private readonly Event _concert;
        private readonly List<Ticket> _tickets;
        private readonly Payment _payment;
        private Boolean _areMerchantsExist;
        private Boolean _areEventsExist;
        private Boolean _areTicketsExist;
        private Boolean _arePaymentsExist;

        public DevelopmentData()
        {
            _merchant = new Merchant
            {
                YandexMoneyAccount = "987654321",
                Place = "Cherdak",
            };
            _concert = new Event
            {
                Artist = "Muse",
                Roubles = 100,
                PressRelease = "Nice band",
                Time = new DateTime(2019, 7, 9, 17, 0, 0, DateTimeKind.Utc),
                PosterUrl = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png",
                Merchant = _merchant
            };
            _tickets = new List<Ticket>
            {
                new Ticket
                {
                    CreatedAt = new DateTime(2019, 7, 9, 16, 0, 0, DateTimeKind.Utc),
                    Number = "1111122222",
                    Expired = false,
                    Roubles = 100,
                    Event = _concert
                },
                new Ticket
                {
                    CreatedAt = new DateTime(2019, 7, 9, 16, 10, 0, DateTimeKind.Utc),
                    Number = "3333344444",
                    Expired = true,
                    Roubles = 100,
                    Event = _concert
                }
            };
            _payment = new Payment
            {
                Amount = 200,
                Email = "test@test.test",
                Tickets = _tickets
            };
        }

        public Boolean IsExistIn(ApplicationContext db)
        {
            _areMerchantsExist = MerchantsAreExist(db);
            _areEventsExist = EventsAreExist(db);
            _areTicketsExist = TicketsAreExist(db);
            _arePaymentsExist = PaymentsAreExist(db);
            return _areMerchantsExist
                   || _areEventsExist
                   || _areTicketsExist
                   || _arePaymentsExist;
        }

        public void InsertTo(ApplicationContext db)
        {
            db.Merchants.Add(_merchant);
            db.Events.Add(_concert);
            db.Tickets.AddRange(_tickets);
            db.Payments.Add(_payment);
            db.SaveChanges();
        }

        private Boolean MerchantsAreExist(ApplicationContext db)
        {
            return db.Merchants.Any(m =>
                m.YandexMoneyAccount == _merchant.YandexMoneyAccount
            );
        }

        private Boolean EventsAreExist(ApplicationContext db)
        {
            return db.Events.Any(e =>
                e.Artist == _concert.Artist
            );
        }

        private Boolean TicketsAreExist(ApplicationContext db)
        {
            return db.Tickets.Any(t =>
                t.Number == _tickets[0].Number
                || t.Number == _tickets[1].Number
            );
        }

        private Boolean PaymentsAreExist(ApplicationContext db)
        {
            return db.Payments.Any(p =>
                p.Email == _payment.Email
            );
        }
    }
}
