namespace ComicsShop.Catalog.Api.Infrastructure.Interfaces.Methods;

public interface IGet<T> where T : class
{
    /// <summary>
    /// Get entity by query string
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    Task<T> GetAsync(QueryString query, CancellationToken cancellationToken);
}