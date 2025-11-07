# ---------------------------
# ETAPA 1 - Runtime Base
# ---------------------------
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# ---------------------------
# ETAPA 2 - Build e Publish
# ---------------------------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia tudo para dentro do container
COPY . .

# Restaura dependências
RUN dotnet restore "TrackingCodeAPI.csproj"

# Compila e publica em modo Release
RUN dotnet publish "TrackingCodeAPI.csproj" -c Release -o /app/publish

# ---------------------------
# ETAPA 3 - Imagem Final
# ---------------------------
FROM base AS final
WORKDIR /app

# Copia o resultado da build
COPY --from=build /app/publish .

# Define o ponto de entrada da aplicação
ENTRYPOINT ["dotnet", "TrackingCodeAPI.dll"]
