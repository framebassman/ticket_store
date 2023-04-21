using Microsoft.AspNetCore.Http.Headers;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddSpaStaticFiles(configuration => {
    configuration.RootPath = "Client/build";
});
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapFallbackToFile("index.html");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

var spaPath = "/";
if (app.Environment.IsDevelopment())
{
    app.MapWhen(y => y.Request.Path.StartsWithSegments(spaPath), client =>
    {
        client.UseSpa(spa =>
        {
            spa.UseProxyToSpaDevelopmentServer("https://localhost:3000");
        });
    });
}
else
{
    app.Map(new PathString(spaPath), client =>
    {
        client.UseSpaStaticFiles();
        client.UseSpa(spa => {
            spa.Options.SourcePath = "Client";

            // adds no-store header to index page to prevent deployment issues (prevent linking to old .js files)
            // .js and other static resources are still cached by the browser
            spa.Options.DefaultPageStaticFileOptions = new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    ResponseHeaders headers = ctx.Context.Response.GetTypedHeaders();
                    headers.CacheControl = new CacheControlHeaderValue
                    {
                        NoCache = true,
                        NoStore = true,
                        MustRevalidate = true
                    };
                }
            };
        });
    });
}
app.Run();