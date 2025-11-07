# =========================
# Etapa 1 — Imagem base (runtime)
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Instala libs necessárias (Oracle, compatibilidade, etc)
RUN apk add --no-cache \
    libc6-compat \
    libaio

# =========================
# Etapa 2 — Build
# =========================
FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia apenas o .csproj para restaurar dependências (melhora o cache)
COPY ["TrackingCodeAPI.csproj", "./"]
RUN dotnet restore "./TrackingCodeAPI.csproj"

# Copia todo o código-fonte
COPY . .
WORKDIR "/src"

# 🔹 Limpa arquivos antigos e recompila do zero
RUN dotnet clean "./TrackingCodeAPI.csproj" -c "$BUILD_CONFIGURATION"
RUN rm -rf /src/bin /src/obj

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

# Cria um usuário não-root para segurança
RUN addgroup -g 1001 -S appuser && \
    adduser -S appuser -G appuser -u 1001 && \
    chown -R appuser:appuser /app

USER appuser

# Define o ponto de entrada
ENTRYPOINT ["dotnet", "TrackingCodeAPI.dll"]
