using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web1
{
    public static class HelloWorldMiddlewareExtensions
    {
        public static void UseHelloWorld(this IApplicationBuilder app)
        {
            app.UseMiddleware<HelloWorldMiddleware>();
        }
    }

    public class HelloWorldMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly HelloWorldMessage msg;

        // "Scoped" SERVICE SHOULDN'T DO CONSTRUCTOR DI!!
        public HelloWorldMiddleware(RequestDelegate next, IOptions<HelloWorldMessage> msg)
        {
            _next = next;
            this.msg = msg.Value;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await context.Response.WriteAsync("Hello");

            await _next(context);

            await context.Response.WriteAsync(this.msg.Message);
        }
    }
}
