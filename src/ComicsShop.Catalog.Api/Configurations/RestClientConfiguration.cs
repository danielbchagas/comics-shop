using ComicsShop.Catalog.Api.Infrastructure.Services;

namespace ComicsShop.Catalog.Api.Configurations
{
    public static class RestClientConfiguration
    {
        public static void RestClientConfigure(this WebApplicationBuilder builder)
        {
            builder.Services.AddHttpClient<ComicsService>(options =>
            {
                options.BaseAddress = new Uri(builder.Configuration.GetSection("MarvelApi:BaseUrl").Value ?? string.Empty);
            });
        }
    }
}
