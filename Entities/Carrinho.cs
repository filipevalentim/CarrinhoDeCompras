namespace CarrinhoDeCompras.Entities
{
    public class Carrinho
        {
            public Guid UsuarioId { get; set; }
            public List<Produto> Produtos { get; set; }
        }
}
