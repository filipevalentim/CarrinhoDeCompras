namespace CarrinhoDeCompras;

using Microsoft.AspNetCore.Hosting;
using Microsoft.OpenApi.Models;
using Repository;
using Services;

public interface IStartup
{
    IConfiguration Configuration { get; }
    public void ConfigureServices(IServiceCollection service);
    public void Configure(WebApplication app, IWebHostEnvironment environment);
}
public class Startup : IStartup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    /// <inheritdoc/>
    public void ConfigureServices(IServiceCollection service)
    {
        var endpoint = Configuration["Redis:Endpoint"];
        // var password = Configuration["Redis:Password"];
        service.AddEndpointsApiExplorer();
        service.AddScoped<ICacheRepository, RedisRepository>();
        service.AddScoped<CarrinhoService>();
        service.AddControllers();
        // Configurando o Redis instanciado pelo Docker 127.0.0.1:6379
        service.AddStackExchangeRedisCache(options => {
            options.InstanceName = "Redis";
            options.Configuration = Configuration["Redis:Endpoint"];
            // options.ConfigurationOptions = new ConfigurationOptions
            //                                {
            //                                    Password = password
            //                                };
            options.ConfigurationOptions.EndPoints.Add(endpoint);
        });
        service.AddSwaggerGen(swagger => swagger.SwaggerDoc("v1", new OpenApiInfo{Title = "Testando o Redis"}));
    }

    /// <inheritdoc />
    public void Configure(WebApplication app, IWebHostEnvironment environment)
    {
        if(app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseHttpsRedirection();

        // app.MapPost("carrinhos",
        // async (Carrinho carrinho, IDistributedCache redis) =>
        //         await redis.SetStringAsync(carrinho.UsuarioId, JsonSerializer.Serialize(carrinho)));


        app.MapControllers();
        app.UseRouting();
    }
}
public static class StartupExtensions
{
    public static WebApplicationBuilder UseStartup<TStartup>(this WebApplicationBuilder webAppBuilder)
        where TStartup : IStartup
    {
        var startup = Activator.CreateInstance(typeof(TStartup), webAppBuilder.Configuration) as IStartup;
        if(startup == null)
        {
            throw new ArgumentException("Classe Startup.cs inválida!");
        }

        startup.ConfigureServices(webAppBuilder.Services);
        var app = webAppBuilder.Build();

        startup.Configure(app, app.Environment);
        app.Run();

        return webAppBuilder;
    }
}
