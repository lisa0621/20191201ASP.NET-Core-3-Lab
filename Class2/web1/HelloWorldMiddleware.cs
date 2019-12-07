using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
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

        // "Scoped" SERVICE SHOULDN'T DO CONSTRUCTOR DI!!
        public HelloWorldMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await context.Response.WriteAsync("Hello");

            await _next(context);

            await context.Response.WriteAsync("World");
        }
    }
}
