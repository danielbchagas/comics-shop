using ComicsShop.Catalog.Api.Infrastructure.Interfaces.Services;
using ComicsShop.Catalog.Api.Infrastructure.Services;

namespace ComicsShop.Catalog.Api.Configurations;

public static class DependencyInjectionConfiguration
{
    public static void DependencyInjectionConfirgure(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IComicsService, ComicsService>();
        builder.Services.AddHttpClient<ComicsService>(options =>
        {
            options.BaseAddress = new Uri(builder.Configuration.GetSection("MarvelApi:BaseUrl").Value ?? string.Empty);
        });
    }
}