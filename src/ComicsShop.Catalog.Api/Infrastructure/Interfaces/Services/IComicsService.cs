using ComicsShop.Catalog.Api.Domain.Entities;
using ComicsShop.Catalog.Api.Infrastructure.Interfaces.Methods;

namespace ComicsShop.Catalog.Api.Infrastructure.Interfaces.Services;

public interface IComicsService : IGet<Rootobject>, IGetById<Rootobject>
{
    
}