namespace CarrinhoDeCompras.Controller;

using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;

public class CarrinhoController : ControllerBase
  {
    public CarrinhoController()
    {
      
    }
    public CarrinhoController(CarrinhoService carrinho)
    {
      _carrinho = carrinho;
    }
    private readonly CarrinhoService _carrinho;

    [HttpGet("gets")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Get()
    {
      var carrinhos = _carrinho.GetCarrinhos();
      return Ok(carrinhos);
    }

    [HttpGet("/get")]
    [Route("{id")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Get(Guid id)
    {
      return Ok(await _carrinho.GetCarrinho(id));
    }

    [HttpPost("/set")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Set(Guid id, Carrinho carrinho)
    {
      _carrinho.SetCarrinho(id, carrinho);
      return Ok();
    }

    [HttpDelete("/delete")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Delete(Guid id)
    {
      _carrinho.DeleteCarrinho(id);
      return Ok();
    }
  }