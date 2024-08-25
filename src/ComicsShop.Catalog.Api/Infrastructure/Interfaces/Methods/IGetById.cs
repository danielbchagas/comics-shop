namespace ComicsShop.Catalog.Api.Infrastructure.Interfaces.Methods;

public interface IGetById<T> where T : class
{
    /// <summary>
    /// Get entity by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<T> GetAsync(int id);
}