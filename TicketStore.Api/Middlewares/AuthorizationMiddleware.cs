using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace TicketStore.Api.Middlewares
{
    public class AuthorizationMiddleware
    {
        private const string _token = "Bearer pkR9vfZ9QdER53mf";
        private readonly RequestDelegate _next;

        public AuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var header = context.Request.Headers["Authorization"];
            if (header == _token)
            {
                await _next(context);
            }
            else
            {
                context.Response.ContentType = "text/plain";
                context.Response.StatusCode = 401;
                context.Response.Headers.Add(HeaderNames.Connection, "close");
                await context.Response.WriteAsync("Unauthorized");
            }
        }
    }
}
