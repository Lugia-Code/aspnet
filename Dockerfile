# =========================
# Etapa 1 — Imagem base (runtime)
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

# Porta que o Azure App Service define via variável de ambiente PORT
ENV ASPNETCORE_URLS=http://+:$PORT

# Instala libs necessárias para Oracle ou compatibilidade
RUN apt-get update && \
    apt-get install -y libaio1 && \
    rm -rf /var/lib/apt/lists/*

# =========================
# Etapa 2 — Build
# =========================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia apenas o .csproj para restaurar dependências
COPY ["TrackingCodeAPI.csproj", "./"]
RUN dotnet restore "./TrackingCodeAPI.csproj"

# Copia todo o código-fonte
COPY . .

# Compila o projeto
RUN dotnet build "./TrackingCodeAPI.csproj" -c "$BUILD_CONFIGURATION" -o /app/build

# =========================
# Etapa 3 — Publicação
# =========================
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TrackingCodeAPI.csproj" -c "$BUILD_CONFIGURATION" -o /app/publish /p:UseAppHost=false

# =========================
# Etapa 4 — Runtime final
# =========================
FROM base AS final
WORKDIR /app

# Copia o resultado do publish
COPY --from=publish /app/publish .

# Cria usuário não-root (opcional, pode comentar se houver problemas de permissões)
RUN groupadd -g 1001 appuser && \
    useradd -m -u 1001 -g appuser appuser && \
    chown -R appuser:appuser /app
USER appuser

# Define ponto de entrada
ENTRYPOINT ["dotnet", "TrackingCodeAPI.dll"]
