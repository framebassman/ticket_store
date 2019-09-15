using System;
using System.Threading.Tasks;
using AspNetCore.Yandex.ObjectStorage;

namespace TicketStore.Api.Model.Poster
{
    public class PosterUpdater
    {
        private PosterReader _reader;
        private YandexStorageService _storage;
        private IGuidProvider _guidProvider;
        public PosterUpdater(YandexStorageService storage, PosterReader reader, IGuidProvider guidProvider)
        {
            _reader = reader;
            _storage = storage;
            _guidProvider = guidProvider;
        }

        public async Task<String> Update(Poster poster)
        {
            var guid = _guidProvider.NewGuid();
            var imageName = $"{guid}.jpg";

            var image = _reader.GetImage(poster);
            await _storage.PutObjectAsync(image, imageName);

            var imageUri = GetImageUri(imageName);
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
