using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EvaSecondApp.Hubs;
using MARC_App.Container;
using MARC_App.repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Controller;

namespace MARC_App
{
    public class Startup
    {
        private readonly IConfiguration conf;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public Startup(IConfiguration conf)
        {
            this.conf = conf;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession();
            services.AddControllersWithViews();
            services.AddDbContextPool<dbContainer>(op => op.UseSqlServer(conf.GetConnectionString("myConnection")));
            services.AddScoped<IuserRep, userRep>();
           services.AddScoped<IAdminRep, AdminRep>();
            
            //services.BuildServiceProvider();



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            

            app.UseRouting();
            app.UseAuthentication();

            
          
           // app.UseEndpoints(a => a.MapHub<myHub>("/anyname2"));

            app.UseEndpoints(a => a.MapDefaultControllerRoute());
            //app.UseEndpoints(a => a.MapControllerRoute(name: "myAreas", pattern: "{controller=users}/{action=getbyid}/{id?}"));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
