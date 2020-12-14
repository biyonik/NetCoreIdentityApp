using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreIdentityApp.WebUI.Context;
using NetCoreIdentityApp.WebUI.CustomValidator;
using NetCoreIdentityApp.WebUI.Describers;
using NetCoreIdentityApp.WebUI.Middlewares;
using NetCoreIdentityApp.WebUI.Models;
using NetCoreIdentityApp.WebUI.Options;

namespace NetCoreIdentityApp.WebUI
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            });
            
            // CookieCustomOptions cookieCustomOptions = new CookieCustomOptions();
            // services.ConfigureApplicationCookie(cookieCustomOptions.GetCookieAuthOptions());
            
            services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(_configuration["ConnectionStrings:default"]);
            });
            
            services.AddIdentity<AppUser, AppRole>(IdentityCustomOptions.Options())
                    .AddPasswordValidator<CustomPasswordValidator>()
                    .AddUserValidator<CustomUserValidator>()
                    .AddErrorDescriber<CustomIdentityErrorDescriber>()
                    .AddEntityFrameworkStores<AppIdentityDbContext>();

            // services.AddAuthentication("NetCoreIdentityApp.Application")
            //         .AddCookie("NetCoreIdentityApp.Application",  cookieCustomOptions.GetCookieAuthOptions());
            CookieCustomOptions cookieCustomOptions = new CookieCustomOptions();
            services.ConfigureApplicationCookie(cookieCustomOptions.GetCookieAuthOptions());

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            FileOptionsMiddleware.GetFileOptions(app, env, "node_modules");
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }
    }
}