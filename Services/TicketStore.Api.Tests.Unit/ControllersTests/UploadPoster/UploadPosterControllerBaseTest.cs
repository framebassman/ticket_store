using AspNetCore.Yandex.ObjectStorage;
using Microsoft.Extensions.Options;
using Moq;
using TicketStore.Api.Controllers;
using TicketStore.Api.Model;
using TicketStore.Api.Model.Poster;

namespace TicketStore.Api.Tests.Unit.ControllersTests.UploadPoster
{
    public abstract class UploadPosterControllerBaseTest : ControllersBaseTest<VerifyController>
    {
        protected readonly UploadPosterController Controller;
        protected UploadPosterControllerBaseTest(string databaseName) : base(databaseName)
        {
            var updater = GetUpdater();
            Controller = new UploadPosterController(Logger, updater);
        }

        private PosterUpdater GetUpdater()
        {
            var yandexStorageOptions = new YandexStorageOptions
            {
                Protocol = "https",
                BucketName = "igor-test",
                Location = "ru-central1",
                Endpoint = "storage.yandexcloud.net",
                AccessKey = "grkCpJmlPZxBpysw-D5H",
                SecretKey = "ar50IbK41nNvW4_QaCsyh_8Fd9AZsO2nvKWf6Fp9",
            };
            var options = Options.Create<YandexStorageOptions>(yandexStorageOptions);
            var storage = new YandexStorageService(options);
            var reader = new PosterReader();
            var guidProvider = new Mock<IGuidProvider>();
            guidProvider.Setup(mock => mock.NewGuid()).Returns("g-u-i-d");
            var dbUpdater = new PosterDbUpdater(Db);
            return new PosterUpdater(storage, reader, dbUpdater, guidProvider.Object);
        }
    }
}
