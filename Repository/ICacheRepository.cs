namespace CarrinhoDeCompras.Repository;

public interface ICacheRepository
  {
    Task<T> GetValue<T>(string id);
    Task<IEnumerable<T>> GetCollection<T>(string collectionKey);
    Task SetValue<T>(string id, T obj);
    Task SetCollection<T>(string collectionKey, IEnumerable<T> collection);
    Task Delete<T>(string id);
  }