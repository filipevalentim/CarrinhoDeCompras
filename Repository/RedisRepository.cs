namespace CarrinhoDeCompras.Repository;

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

    public async Task<T> GetValue<T>(Guid id)
    {
      var key = id.ToString().ToLower();
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

    public async Task SetValue<T>(Guid id, T obj)
    {
      var key = id.ToString().ToLower();
      var newValue = JsonSerializer.Serialize(obj);
      await _distributedCache.SetStringAsync(key, newValue);
    }

    public async Task SetCollection<T>(string collectionKey, IEnumerable<T> collection)
    {
      var key = collectionKey.ToString().ToLower();
      var newValue = JsonSerializer.Serialize(collection);
      await _distributedCache.SetStringAsync(key, newValue);
    }

    public async Task Delete<T>(Guid id)
    {
      var key = id.ToString().ToLower();
      await _distributedCache.RemoveAsync(key);
    }
  }
