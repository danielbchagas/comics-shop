using ComicsShop.Catalog.Api.Domain.Entities;
using ComicsShop.Catalog.Api.Domain.Options;
using ComicsShop.Catalog.Api.Infrastructure.Interfaces.Services;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace ComicsShop.Catalog.Api.Infrastructure.Services;

public class ComicsService(IHttpClientFactory clientFactory, IOptions<MarvelApiOption> marvelApiConfig) : IComicsService
{
    private readonly HttpClient _client = clientFactory.CreateClient("ComicsService");
    private readonly MarvelApiOption _marvelApiOption = marvelApiConfig.Value;

    public async Task<Rootobject> GetAsync(QueryString query)
    {
        var url = string.Empty;
        var (hash, timeStamp, publicKey, privateKey) = CreateCredentials();
        
        if (query.HasValue)
            url = $"/v1/public/comics{query.Value}&ts={timeStamp}&apikey={publicKey}&hash={hash}";
        else
            url = $"/v1/public/comics?ts={timeStamp}&apikey={publicKey}&hash={hash}";

        var result = await _client.GetAsync(url);
        
        if (!result.IsSuccessStatusCode)
        {
            throw new Exception("Error");
        }
        
        var content = await result.Content.ReadAsStringAsync();
        var comics = JsonSerializer.Deserialize<Rootobject>(content);
        
        return comics;
    }

    public async Task<Rootobject> GetAsync(int id)
    {
        var (hash, timeStamp, publicKey, privateKey) = CreateCredentials();
        var url = $"/v1/public/comics/{id}?ts={timeStamp}&apikey={publicKey}&hash={hash}";
        
        var result = await _client.GetAsync(url);
        
        if (!result.IsSuccessStatusCode)
        {
            throw new Exception("Error");
        }
        
        var content = await result.Content.ReadAsStringAsync();
        var comics = JsonSerializer.Deserialize<Rootobject>(content);
        
        return comics;
    }

    private (string hash, string timeStamp, string publicKey, string privateKey) CreateCredentials()
    {
        var timeStamp = DateTime.Now.Ticks.ToString();
        var publicKey = _marvelApiOption.PublicKey;
        var privateKey = _marvelApiOption.PrivateKey;
        
        var hash = ComputeHash(timeStamp, publicKey, privateKey);

        return (hash, timeStamp, publicKey, privateKey);
    }

    private string ComputeHash(string ts, string publicKey, string privateKey)
    {
        byte[] encodedSignatureBytes = Encoding.UTF8.GetBytes(ts + privateKey + publicKey);
        MD5 md5 = MD5.Create();
        byte[] signatureHashBytes = md5.ComputeHash(encodedSignatureBytes);
        return BitConverter.ToString(signatureHashBytes)
            .ToLower().Replace("-", string.Empty);
    }
}