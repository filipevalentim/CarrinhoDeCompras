# Usa uma imagem base do ASP.NET Core
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

WORKDIR /app

# Copia os arquivos do projeto para o cont�iner
COPY . .

# Restaura as depend�ncias e constr�i o aplicativo
RUN dotnet resore
RUN dotnet build -c Release -o out

# Publica o aplicativo
FROM build AS publish
RUN dotnet publish -c Release -o out

# Cria a imagem final
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=publish /app/out .

# Define o comando de inicializa��o
CMD ["dotnet", "CarrinhoDeCompras.dll"]
