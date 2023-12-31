﻿namespace CarrinhoDeCompras.Repository;

public interface ICacheRepository
  {
    Task<T> GetValue<T>(Guid id);
    Task<IEnumerable<T>> GetCollection<T>(string collectionKey);
    Task SetValue<T>(Guid id, T obj);
    Task SetCollection<T>(string collectionKey, IEnumerable<T> collection);
    Task Delete<T>(Guid id);
  }