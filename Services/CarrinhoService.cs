﻿namespace CarrinhoDeCompras.Services;

using Entities;
using Repository;

public class CarrinhoService
  {
    private readonly ICacheRepository _redis;
    private const string CACHE_COLLECTION_KEY = "_Carrinhos";

    public CarrinhoService()
    {
    }
    public CarrinhoService(ICacheRepository redis)
    {
      _redis = redis;
    }

    public async Task<Carrinho> GetCarrinho(Guid id)
    {
      var carrinho = await _redis.GetValue<Carrinho>(id);
      if (carrinho == null)
      {
        // var carrinho = await bancodedadosRepository.GetCarrinho(id);
        // await _redis.SetValue(id, carrinho);
        return default;
      }
      return carrinho;
    }

    public async Task<IEnumerable<Carrinho>> GetCarrinhos()
    {
      var carrinhos = await _redis.GetCollection<Carrinho>(CACHE_COLLECTION_KEY);
      if (carrinhos is null || !carrinhos.Any())
      {
        // var carrinho = await bancodedadosRepository.GetCarrinho(id);
        // await _redis.SetValue(id, carrinho);
        return default;
      }
      return carrinhos;
    }

    public Task SetCarrinho(Guid id, Carrinho carrinho)
    {
      return _redis.SetValue(id, carrinho);
    }

    public Task DeleteCarrinho(Guid id)
    {
      return _redis.Delete<Carrinho>(id);
    }
  }