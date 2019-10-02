using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using TicketStore.Data;

namespace TicketStore.Api.Model.Poster
{
    public class PosterDbUpdater : IPosterDbUpdater
    {
        private readonly ApplicationContext _db;
        private readonly ILogger<PosterDbUpdater> _log;
        public PosterDbUpdater(ApplicationContext context, ILogger<PosterDbUpdater> log)
        {
            _db = context;
            _log = log;
        }

        public bool CanUpdate(Poster poster)
        {
            var concert = _db.Events.FirstOrDefault(e => e.Id == poster.eventId);
            return concert != null;
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
            _db.SaveChanges();
            _log.LogInformation("Event updated: {@concert}", concert);
        }
    }
}
