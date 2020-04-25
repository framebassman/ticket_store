using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TicketStore.Api.Middlewares;
using Xunit;

namespace TicketStore.Api.Tests.Unit.MiddlewaresTests
{
    public class AuthorizationMiddlewareTests
    {
        private AuthorizationMiddleware _middleware;
        private KeyValuePair<String, String> _authHeader;

        public AuthorizationMiddlewareTests()
        {
            _middleware = new AuthorizationMiddleware(Next);
            _authHeader = new KeyValuePair<string, string>("Authorization", "Bearer pkR9vfZ9QdER53mf");
            
        }
        
        [Fact]
        public async Task WithoutAuthHeader_ReturnsNonAuthorized()
        {
            // Arrange
            HttpContext context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            // Act
            await _middleware.Invoke(context);
            
            // Assert
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            using (var reader = new StreamReader(context.Response.Body))
            {
                var response = await reader.ReadToEndAsync();
                Assert.Equal((int) HttpStatusCode.Unauthorized, context.Response.StatusCode);
                Assert.Equal("application/json", context.Response.ContentType);
                Assert.Equal("{\"message\":\"unauthorized\"}", response);
            }
        }
        
        [Fact]
        public async Task WithAuthHeader_ReturnsOk()
        {
            // Arrange
            HttpContext context = new DefaultHttpContext();
            context.Request.Headers.Add(_authHeader.Key, _authHeader.Value);
            context.Response.Body = new MemoryStream();

            // Act
            await _middleware.Invoke(context);
            
            // Assert
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            using (var reader = new StreamReader(context.Response.Body))
            {
                var response = await reader.ReadToEndAsync();
                Assert.Equal((int) HttpStatusCode.OK, context.Response.StatusCode);
                Assert.Equal("", response);
            }
        }
        
        private Task Next(HttpContext innerHttpContext)
        {
            return Task.FromResult(0);
        }
    }
}
