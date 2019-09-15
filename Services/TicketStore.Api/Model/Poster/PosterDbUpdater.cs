using System;
using System.Linq;
using TicketStore.Data;

namespace TicketStore.Api.Model.Poster
{
    public class PosterDbUpdater : IPosterDbUpdater
    {
        private readonly ApplicationContext _db;
        public PosterDbUpdater(ApplicationContext context)
        {
            _db = context;
        }

        public void Update(Poster poster, String imageUri)
        {
            var concert = _db.Events.FirstOrDefault(e => e.Id == poster.eventId);
            if (concert == null)
            {
                throw new Exception($"No concert found by ID: {poster.eventId}");
            }
            concert.PosterUrl = imageUri;
            _db.Events.Update(concert);
        }
    }
}
