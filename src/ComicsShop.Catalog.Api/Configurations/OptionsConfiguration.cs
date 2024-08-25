using ComicsShop.Catalog.Api.Domain.Options;

namespace ComicsShop.Catalog.Api.Configurations
{
    public static class OptionsConfiguration
    {
        public static void OptionsConfigurationConfigure(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<MarvelApiOption>(builder.Configuration.GetSection("MarvelApi"));
        }
    }
}
