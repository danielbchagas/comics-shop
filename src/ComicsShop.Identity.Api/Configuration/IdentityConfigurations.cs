using ComicsShop.Identity.Api.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ComicsShop.Identity.Api.Configuration
{
    public static class IdentityConfigurations
    {
        public static void IdentityConfigure(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<ApplicationDbContext>(
                options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
                    r => r.EnableRetryOnFailure(maxRetryCount: 5)));

            builder.Services.AddIdentityApiEndpoints<IdentityUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
        }
    }
}
