using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace web1 {
    public class Startup {
        private readonly IConfiguration Configuration;

        public Startup(IConfiguration Configuration)
        {            
            this.Configuration = Configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices (IServiceCollection services) {
            // services.AddSingleton<HelloWorldMessage> (new HelloWorldMessage () {
            //     Message = "Will"
            // });

            // services.AddSingleton<HelloWorldMessage> ((sp) => {
            //     return new HelloWorldMessage () {
            //     Message = "John"
            //     };
            // });

            // services.Configure<HelloWorldMessage>((msg) =>{
            //     msg.Message = "Tom";
            // });

            services.Configure<HelloWorldMessage>(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            
            app.UseHelloWorld ();

            app.Use (async (context, next) => {
                await context.Response.WriteAsync ("123");
                await next ();
                await context.Response.WriteAsync ("456");
            });

            //app.Use(async (context, next) => {
            //    await context.Response.WriteAsync("Hello");
            //    await next();
            //    await context.Response.WriteAsync("World");
            //});

            app.Run (async (context) => {
                await context.Response.WriteAsync ("Will");
            });

            // if (env.IsDevelopment ()) {
            //     app.UseDeveloperExceptionPage ();
            // }

            // app.UseRouting ();

            // app.UseStatusCodePages (async context => {
            //     context.HttpContext.Response.ContentType = "text/plain";

            //     await context.HttpContext.Response.WriteAsync (
            //         "Status code page, status code: " +
            //         context.HttpContext.Response.StatusCode);
            // });

            // app.UseEndpoints (endpoints => {
            //     endpoints.MapGet ("/", async context => {
            //         await context.Response.WriteAsync ("Hello World!");
            //     });
            // });
        }
    }
}