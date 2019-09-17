using System;
using System.Threading.Tasks;
using AspNetCore.Yandex.ObjectStorage;
using Microsoft.Extensions.Logging;

namespace TicketStore.Api.Model.Poster
{
    public class PosterUpdater : IPosterUpdater
    {
        private readonly ILogger<PosterUpdater> _log;
        private YandexStorageService _storage;
        private IPosterReader _reader;
        private IPosterDbUpdater _dbUpdater;
        private IGuidProvider _guidProvider;
        public PosterUpdater(ILogger<PosterUpdater> log, YandexStorageService storage, IPosterReader reader, IPosterDbUpdater dbUpdater, IGuidProvider guidProvider)
        {
            _storage = storage;
            _reader = reader;
            _dbUpdater = dbUpdater;
            _guidProvider = guidProvider;
            _log = log;
        }

        public async Task<String> Update(Poster poster)
        {
            if (!_dbUpdater.CanUpdate(poster)) {
                throw new Exception($"Can't update concert with ID: {poster.eventId}");
            }
            
            var image = _reader.GetImage(poster);
            _log.LogInformation($"Image compressed from {poster.imageUrl}");

            var guid = _guidProvider.NewGuid();
            var imageName = $"{guid}.jpg";
            _log.LogInformation("Image name: {@imageName}", imageName);

            var imageUrl = await _storage.PutObjectAsync(image, imageName);
            _log.LogInformation("Image uploaded to Yandex Object Storage");
        
            _log.LogInformation("Image URL: {@imageUrl}", imageUrl);
            _dbUpdater.Update(poster, imageUrl);
            return imageUrl;
        }
    }
}
