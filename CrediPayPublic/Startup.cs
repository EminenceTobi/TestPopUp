using CrediPayPublic.Models.BLL.Redis;
using CrediPayPublic.Models.BLL.ServiceBusConsumer;
using CrediPayPublic.Models.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrediPayPublic
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
            var mvcBuilder = services.AddControllersWithViews();
#if DEBUG
            mvcBuilder.AddRazorRuntimeCompilation();
#endif
            services.AddResponseCaching();
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            //services.AddControllers();
            services.AddHttpContextAccessor();
            services.AddDistributedMemoryCache();
            services.AddSession(opt =>
            {
                opt.Cookie.IsEssential = true;
            });
            services.AddMvc().AddSessionStateTempDataProvider();
            //  services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IRedisCache, RedisCache>();
            services.AddScoped<IServiceBusConsumer, ServiceBusConsumer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceBusConsumer busConsumer)
        {
            busConsumer.RegisterOnMessageHandlerAndReceiveMessages();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
             endpoints.MapControllerRoute(
                     name: "default",
                     pattern: "{controller=PopUp}/{action=popup}/{id?}");
             endpoints.MapControllerRoute(
                    name: "payment-link",
                    pattern: "payment-link/{*id}",
                    defaults: new { controller = "PaymentLink", action = "PaymentLink" });
             endpoints.MapControllerRoute(
                  name: "testing",
                  pattern: "testing/{*id}",
                  defaults: new { controller = "PaymentLink", action = "Testing" });
            });
        }
    }
}