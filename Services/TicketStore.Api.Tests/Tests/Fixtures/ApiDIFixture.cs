using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketStore.Api.Tests.Data;
using TicketStore.Api.Tests.Model.Services;
using Xunit.Microsoft.DependencyInjection;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace TicketStore.Api.Tests.Tests.Fixtures;

public class ApiDIFixture : TestBedFixture
{
    protected override void AddServices(IServiceCollection services, IConfiguration? configuration)
        => services
            .AddTransient<ApiService>()
            .AddTransient<FakeSenderService>()
            .AddTransient<WebService>()
            .AddTransient<ApplicationContext>();

    protected override ValueTask DisposeAsyncCore()
        => new();

    protected override IEnumerable<TestAppSettings> GetTestAppSettings()
    {
        yield return new() { Filename = "appsettings.json", IsOptional = false };
    }
}
