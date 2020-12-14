using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetCoreIdentityApp.WebUI.Models;

namespace NetCoreIdentityApp.WebUI.Context
{
    public class AppIdentityDbContext: IdentityDbContext<AppUser, AppRole, string>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> dbContextOptions): base(dbContextOptions)
        {

        }
    }
}