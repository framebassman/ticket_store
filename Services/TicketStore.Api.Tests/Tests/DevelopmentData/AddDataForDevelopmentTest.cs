using System;
using TicketStore.Api.Tests.Data;
using Xunit;
using Xunit.Abstractions;

namespace TicketStore.Api.Tests.Tests.DevelopmentData
{
    public class AddDataForDevelopmentTest : IDisposable
    {
        private ITestOutputHelper _log;
        private ApplicationContext _db;

        public AddDataForDevelopmentTest(ITestOutputHelper log)
        {
            _log = log;
            _db = new ApplicationContext();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    
        [Trait("Category", "DevelopmentData")]
        [Fact]
        public void AddDevelopmentData()
        {
            var developmentData = new DevelopmentData();
            if (developmentData.IsExistIn(_db))
            {
                _log.WriteLine("Development data has already existed");
                Assert.Equal(0, 1);
            }
            else
            {
                _log.WriteLine("Insert data for development");
                developmentData.InsertTo(_db);
            }
        }
    }
}
