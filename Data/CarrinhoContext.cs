namespace CarrinhoDeCompras.Data;

using Entities;

public class CarrinhoContext : DbContext
  {
    public DbSet<Carrinho> carrinho { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlite(connectionString: "DataSource=app.db;Cache=Shared");
    }
  }
