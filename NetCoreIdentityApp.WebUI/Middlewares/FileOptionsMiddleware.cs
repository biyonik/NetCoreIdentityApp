using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;

namespace NetCoreIdentityApp.WebUI.Middlewares
{
    public static class FileOptionsMiddleware
    {
        public static void GetFileOptions(IApplicationBuilder app, IWebHostEnvironment environment, string path)
        {
            string root = Path.Combine(environment.ContentRootPath, path);
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(root),
                RequestPath = "/" + path
            });
        }
    }
}