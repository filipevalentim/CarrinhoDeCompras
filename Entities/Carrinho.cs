namespace CarrinhoDeCompras.Entities
{
    record Carrinho(string UsuarioId, List<Produto> Produtos);
}