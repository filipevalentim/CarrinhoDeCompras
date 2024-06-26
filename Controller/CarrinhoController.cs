﻿namespace CarrinhoDeCompras.Controller;

using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;

[ApiController]
[Route("api/[controller]/[action]")]
public class CarrinhoController : ControllerBase
  {
    public CarrinhoController(CarrinhoService carrinho)
    {
      _carrinho = carrinho;
    }
    private readonly CarrinhoService _carrinho;

    [HttpGet]
    public async Task<IActionResult> GetAll(string key)
    {
      List<string> lista = new List<string>();
      var carrinhos = _carrinho.GetCarrinhos(key);
      lista.Add(carrinhos.ToString());
      return Ok(lista);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] string id)
    {
      return Ok(await _carrinho.GetCarrinho(id));
    }

    [HttpPost]
    public async Task<IActionResult> Set(Carrinho carrinho)
    {
      await _carrinho.SetCarrinho(carrinho);
      return Ok(carrinho);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] string id)
    {
      _carrinho.DeleteCarrinho(id);
      return Ok();
    }
  }