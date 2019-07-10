using System;
using System.Linq;
using Npgsql;
using TicketStore.Api.Tests.Data;
using TicketStore.Api.Tests.Environment;
using Xunit;

namespace TicketStore.Api.Tests.Tests
{
    public class DbTests
    {
//        [Fact]
//        public void TestRawConnection()
//        {
//            var answer = 0;
//            
//            var connectionString =
//                $"Host={new AppHost().Value()};Port=5432;Database=store_db;Username=store_user;Password=KqCQzyH2akGB9gQ4";
//
//            using (var conn = new NpgsqlConnection(connectionString))
//            {
//                conn.Open();
//
//                using (var cmd = new NpgsqlCommand("SELECT COUNT(*) FROM tickets;", conn))
//                using (var reader = cmd.ExecuteReader())
//                {
//                    while (reader.Read())
//                    {
//                        Console.WriteLine(reader.GetInt32(0));
//                        answer += reader.GetInt32(0);
//                    }                    
//                }
//            }
//            
//            Assert.Equal(0, answer);
//        }
//
//        [Fact]
//        public void TestEFConnection()
//        {
//            var answer = 0;
//            using (var db = new ApplicationContext())
//            {
//                answer += db.Events.Count();
//                var result = db.Events.ToList();
//            }
//            Assert.Equal(0, answer);
//        }
    }
}
