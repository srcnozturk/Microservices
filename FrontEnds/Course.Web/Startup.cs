using Course.Web.Handler;
using Course.Web.Models;
using Course.Web.Services;
using Course.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Services;
using System;

namespace Course.Web
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
            
            services.Configure<ClientSettings>(Configuration.GetSection("ClientSettings"));
            services.AddHttpClient<IIdentityService,IdentityService>();
            services.AddHttpContextAccessor();
            services.Configure<ServiceApiSettings>(Configuration.GetSection("ServiceApiSettings"));
            var serviceApiSettings =Configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();
            services.AddScoped<ResourceOwnerPasswordTokenHandler>();
            services.AddHttpClient<IClientCrediantialTokenService,ClientCrediantialTokenService>();

            services.AddScoped<ISharedIdentityService, SharedIdentityService>();
            services.AddHttpClient<ICatalogService, CatalogService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUrl}/{serviceApiSettings.Catalog.Path}");
            });

            services.AddHttpClient<IUserService, UserService>(opt =>
            {
                opt.BaseAddress = new Uri(serviceApiSettings.IdentityBaseUrl);
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();


            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opts =>
                {
                    opts.LoginPath         = "/Auth/SignIn";
                    opts.ExpireTimeSpan    = TimeSpan.FromDays(1);
                    opts.SlidingExpiration = true;  //Giriş yaptıkça süre uzatılsın mı ?
                    opts.Cookie.Name       = "udemywebcookie";
                });


            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
