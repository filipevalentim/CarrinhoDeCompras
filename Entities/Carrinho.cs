namespace CarrinhoDeCompras.Entities
{
    public record Carrinho(string UsuarioId, List<Produto> Produtos);
}
