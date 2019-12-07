using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace web4
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //middleware Handle了 return 重來
            //是不是開發環境
            if (env.IsDevelopment())
            {
                //顯示例外頁面資訊
                app.UseDeveloperExceptionPage();
            }

            //不是Https導頁為Https，重來
            app.UseHttpsRedirection();

            //是不是靜態檔實體路徑wwwroot，擺這沒有權限
            ////app.UseStaticFiles();

            //路由
            app.UseRouting();

            //是否有權限
            app.UseAuthorization();

            //一定擺在最後
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
