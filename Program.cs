namespace CarrinhoDeCompras
{
    using System.Text.Json;
    using CarrinhoDeCompras.Entities;
    using Microsoft.Extensions.Caching.Distributed;

    internal class Program
  {
    private static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      // Add services to the container.
      // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
      builder.Services.AddEndpointsApiExplorer();

      // Configurando o Redis instanciado pelo Docker 127.0.0.1:6379
      builder.Services.AddStackExchangeRedisCache(options =>
      {
        options.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
      });
      builder.Services.AddSwaggerGen();

      var app = builder.Build();

      // Configure the HTTP request pipeline.
      if (app.Environment.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI();
      }

      app.UseHttpsRedirection();

      // Rota para adicionar carrinho no Redis
      app.MapPost("carrinhos", async (Carrinho carrinho, IDistributedCache redis) =>
      {
        await redis.SetStringAsync(carrinho.UsuarioId, JsonSerializer.Serialize(carrinho));
        return true;
      });

      // Rota para obter usuarioId do Redisd
      app.MapGet("/carrinhos/{usuarioId}", async (string usuarioId, IDistributedCache redis) =>
      {
        var data = await redis.GetStringAsync(usuarioId);

        if (string.IsNullOrEmpty(data))
        {
          return null;
        }

        var carrinho = JsonSerializer.Deserialize<Carrinho>(data, new JsonSerializerOptions
        {
          PropertyNameCaseInsensitive = false,
        });
        return carrinho;
      });

      app.Run();
    }
  }
}