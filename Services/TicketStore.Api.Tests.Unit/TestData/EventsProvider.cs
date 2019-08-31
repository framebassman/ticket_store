using System;
using TicketStore.Data.Model;

namespace TicketStore.Api.Tests.Unit.TestData
{
    public class EventsProvider
    {
        private readonly Merchant _merchant;

        public EventsProvider(Merchant merchant)
        {
            _merchant = merchant;
        }

        public Event WithDate(DateTime date)
        {
            return new Event
            {
                Artist = "Test artist",
                Roubles = 1.00m,
                PressRelease = "Test press release",
                Time = date,
                PosterUrl = "https://ya.ru/logo.png",
                MerchantId = _merchant.Id,
                Merchant = _merchant
            };
        }
    }
}