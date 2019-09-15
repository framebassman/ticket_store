using System;
using System.Threading.Tasks;
using AspNetCore.Yandex.ObjectStorage;

namespace TicketStore.Api.Model.Poster
{
    public class PosterUpdater
    {
        private YandexStorageService _storage;
        private PosterReader _reader;
        private PosterDbUpdater _dbUpdater;
        private IGuidProvider _guidProvider;
        public PosterUpdater(YandexStorageService storage, PosterReader reader, PosterDbUpdater dbUpdater, IGuidProvider guidProvider)
        {
            _storage = storage;
            _reader = reader;
            _dbUpdater = dbUpdater;
            _guidProvider = guidProvider;
        }

        public async Task<String> Update(Poster poster)
        {
            var guid = _guidProvider.NewGuid();
            var imageName = $"{guid}.jpg";

            var image = _reader.GetImage(poster);
            await _storage.PutObjectAsync(image, imageName);

            var imageUri = GetImageUri(imageName);
            _dbUpdater.Update(poster, imageUri);
            return imageUri;
        }

        private String GetImageUri(String imageName)
        {
            var protocol = ApiConfiguration.YandexOsProtocol;
            var host = ApiConfiguration.YandexOsEndpoint;
            var bucket = ApiConfiguration.YandexOsPostersBucketName;
            return $"{protocol}://{host}/{bucket}/{imageName}";
        }
    }
}
