using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreTest.Business;
using CoreTest.Infrastructure.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreTest
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Using Strongly-Typed Confguration
            services.AddOptions();
            services.Configure<MyOptions>(Configuration);
            var section = services.Configure<MySubOptions>(Configuration.GetSection("MyComplexConfiguration"));

            // pokud chceme prenaset konfiguraci primo do controleru, pridame sekci do services 
            // pouzije se pak v HomeControlleru v kontruktoru
            var config = new MySubOptions();
            Configuration.GetSection("MyComplexConfiguration").Bind(config);
            services.AddSingleton(config);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddKendo();

            services.AddTransient<IDateService, TestDateService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Mohou byt jeste tyto konfigurace -> env.IsStaging / env.IsProduction
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // HTML, CSS, JavaScript, and images can be served by an ASP.NET Core application by using functionalities of the Microsoft.AspNetCore.StaticFiles 
            // package and by registering the middleware. This middleware component serves all fles under the wwwroot folder as if they were in the root path
            // of the application.So the / wwwroot / index.html fle will be returned when a request for http://example.com / index.html arrives.
            app.UseStaticFiles(
                // tady je mozne jeste provest konfiguraci dalsich parametru
                // new StaticFileOptions() { FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"MyArchive")), RequestPath = new PathString("/Archive") }
            );

            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
