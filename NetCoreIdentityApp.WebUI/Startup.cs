using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreIdentityApp.WebUI.Context;
using NetCoreIdentityApp.WebUI.CustomValidator;
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

            services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(_configuration["ConnectionStrings:default"]);
            });
            services.AddIdentity<AppUser, AppRole>(IdentityCustomOptions.Options())
                    .AddPasswordValidator<CustomPasswordValidator>()
                    .AddEntityFrameworkStores<AppIdentityDbContext>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            FileOptionsMiddleware.GetFileOptions(app, env, "node_modules");
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
            app.UseAuthentication();
        }
    }
}