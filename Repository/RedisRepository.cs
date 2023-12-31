namespace CarrinhoDeCompras.Repository;

using System.Net.Mime;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

public class RedisRepository : ICacheRepository
  {
    private readonly IDistributedCache _distributedCache;

    public RedisRepository()
    {
      
    }
    public RedisRepository(IDistributedCache distributedCache)
    {
      _distributedCache = distributedCache;
    }

    public async Task<T> GetValue<T>(string id)
    {
      var key = id.ToLower();
      var result = await _distributedCache.GetStringAsync(key);
      if (string.IsNullOrEmpty(result))
      {
        return default;
      }
      return JsonSerializer.Deserialize<T>(result);
    }

    public async Task<IEnumerable<T>> GetCollection<T>(string collectionKey)
    {
      var result = await _distributedCache.GetStringAsync(collectionKey);
      if (string.IsNullOrEmpty(result))
      {
        return default;
      }
      return JsonSerializer.Deserialize<IEnumerable<T>>(result);
    }

    public async Task SetValue<T>(string id, T obj)
    {
      var key = id.ToLower();
      var newValue = JsonSerializer.Serialize(obj);
      await _distributedCache.SetStringAsync(key, newValue);
    }

    public async Task SetCollection<T>(string collectionKey, IEnumerable<T> collection)
    {
      var key = collectionKey.ToString().ToLower();
      var newValue = JsonSerializer.Serialize(collection);
      await _distributedCache.SetStringAsync(key, newValue);
    }

    public async Task Delete<T>(string id)
    {
      var key = id.ToLower();
      await _distributedCache.RemoveAsync(key);
    }
  }
