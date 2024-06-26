using TicketStore.Web.Model;
using NHamcrest;
using Xunit;
using Assert = NHamcrest.XUnit.Assert;

namespace TicketStore.Web.Tests.Unit.ModelTests;

public class DateTimeProviderTest
{
    [Fact]
    public void ProviderReturnsTimeInUtc()
    {
        var time = new CustomStuff().Now;
        Assert.That(time.Kind, Is.EqualTo(DateTimeKind.Utc));
    }
}
