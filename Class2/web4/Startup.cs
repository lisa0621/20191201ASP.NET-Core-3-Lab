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
            //middleware Handle�F return ����
            //�O���O�}�o����
            if (env.IsDevelopment())
            {
                //��ܨҥ~������T
                app.UseDeveloperExceptionPage();
            }

            //���OHttps�ɭ���Https�A����
            app.UseHttpsRedirection();

            //�O���O�R�A�ɹ�����|wwwroot�A�\�o�S���v��
            ////app.UseStaticFiles();

            //����
            app.UseRouting();

            //�O�_���v��
            app.UseAuthorization();

            //�@�w�\�b�̫�
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
