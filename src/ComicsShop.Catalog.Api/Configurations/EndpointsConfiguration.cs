using ComicsShop.Catalog.Api.Domain.Options;
using ComicsShop.Catalog.Api.Infrastructure.Interfaces.Services;
using Microsoft.Extensions.Options;

namespace ComicsShop.Catalog.Api.Configurations
{
    public static class EndpointsConfiguration
    {
        public static void EndpointsConfigure(this WebApplication app)
        {
            var service = app.Services.GetRequiredService<IComicsService>();
            var options = app.Services.GetRequiredService<IOptions<MarvelApiOption>>();

            app.MapGet("/comics", async (string query = "?limit=10&offset=0") =>
            {
                var result = await service.GetAsync(new QueryString(query));
                return result;
            })
            .WithName("GetComics")
            .WithOpenApi();

            app.MapGet("/comics/{id}", async (int id) =>
            {
                var result = await service.GetAsync(id);
                return result;
            })
            .WithName("GetComicsById")
            .WithOpenApi();
        }
    }
}
