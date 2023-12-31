namespace CarrinhoDeCompras.Repository;

using System.Net.Mime;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

public class RedisRepository : ICacheRepository
  {
    private readonly IDistributedCache _cache;
    private readonly DistributedCacheEntryOptions _options;

    public RedisRepository(IDistributedCache cache)
    {
      _cache = cache;
      _options = new DistributedCacheEntryOptions
                 {
                   AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3600), SlidingExpiration = TimeSpan.FromSeconds(1200),
                 };
    }
    
    public async Task<T> GetValue<T>(string id)
    {
      var key = id.ToLower();
      var result = await _cache.GetStringAsync(key);
      if (string.IsNullOrEmpty(result))
      {
        return default;
      }
      return JsonSerializer.Deserialize<T>(result);
    }

    public async Task<IEnumerable<T>> GetCollection<T>(string collectionKey)
    {
      var result = await _cache.GetStringAsync(collectionKey);
      if (string.IsNullOrEmpty(result))
      {
        return default;
      }
      return JsonSerializer.Deserialize<IEnumerable<T>>(result).ToList();
    }

    public async Task SetValue<T>(string id, T obj)
    {
      var key = id.ToLower();
      var newValue = JsonSerializer.Serialize(obj);
      await _cache.SetStringAsync(key, newValue, _options);
    }

    public async Task SetCollection<T>(string collectionKey, IEnumerable<T> collection)
    {
      var key = collectionKey.ToString().ToLower();
      var newValue = JsonSerializer.Serialize(collection);
      await _cache.SetStringAsync(key, newValue, _options);
    }

    public async Task Delete<T>(string id)
    {
      var key = id.ToLower();
      await _cache.RemoveAsync(key);
    }
  }
