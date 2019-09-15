using System;
using System.Threading.Tasks;
using AspNetCore.Yandex.ObjectStorage;

namespace TicketStore.Api.Model.Poster
{
    public class PosterUpdater
    {
        private PosterReader _reader;
        private YandexStorageService _storage;
        public PosterUpdater(YandexStorageService storage, PosterReader reader)
        {
            _reader = reader;
            _storage = storage;
        }

        public async Task<String> Update(Poster poster)
        {
            var image = _reader.GetImage(poster);
            var imageName = $"{Guid.NewGuid()}.jpg";
            await _storage.PutObjectAsync(image, imageName);
            return imageName;
        }
    }
}
