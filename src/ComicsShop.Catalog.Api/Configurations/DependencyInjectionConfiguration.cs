using ComicsShop.Catalog.Api.Infrastructure.Interfaces.Services;
using ComicsShop.Catalog.Api.Infrastructure.Services;

namespace ComicsShop.Catalog.Api.Configurations;

public static class DependencyInjectionConfiguration
{
    public static void DependencyInjectionConfirgure(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IComicsService, ComicsService>();
    }
}