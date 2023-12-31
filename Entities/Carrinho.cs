namespace CarrinhoDeCompras.Entities
{
    public class Carrinho
        {
            public string UsuarioId { get; set; }
            public List<Produto> Produtos { get; set; }
        }
}
