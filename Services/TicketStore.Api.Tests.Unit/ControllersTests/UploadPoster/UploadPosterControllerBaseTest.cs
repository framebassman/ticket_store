using AspNetCore.Yandex.ObjectStorage;
using Microsoft.Extensions.Options;
using TicketStore.Api.Controllers;

namespace TicketStore.Api.Tests.Unit.ControllersTests.UploadPoster
{
    public abstract class UploadPosterControllerBaseTest : ControllersBaseTest<VerifyController>
    {
        protected readonly UploadPosterController Controller;
        protected UploadPosterControllerBaseTest(string databaseName) : base(databaseName)
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
            Controller = new UploadPosterController(Db, Logger, storage);
        }
    }
}
