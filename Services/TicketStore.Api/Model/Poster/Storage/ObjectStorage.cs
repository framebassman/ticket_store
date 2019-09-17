using System;
using AspNetCore.Yandex.ObjectStorage;
using Microsoft.Extensions.Configuration;

namespace TicketStore.Api.Model.Poster.Storage
{
    public class ObjectStorage
    {
        private readonly IConfigurationSection _json;

        public ObjectStorage(IConfigurationSection json)
        {
            _json = json;
        }

        public YandexStorageOptions Options()
        {
            return new YandexStorageOptions
            {
                Protocol = _json.GetValue<String>("Protocol"),
                Endpoint = _json.GetValue<String>("Endpoint"),
                Location = _json.GetValue<String>("Location"),
                BucketName = _json.GetValue<String>("BucketName"),
                AccessKey = _json.GetValue<String>("AccessKey"),
                SecretKey = _json.GetValue<String>("SecretKey")
            };
        }
    }
}
