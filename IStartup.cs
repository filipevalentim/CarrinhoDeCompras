namespace CarrinhoDeCompras;

public interface IStartup
  {
    IConfiguration Configuration { get; }
    public void ConfigureServices(IServiceCollection service);
    public void Configure(WebApplication app, IWebHostEnvironment environment);
  }
