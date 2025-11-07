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

# 🔹 Copia o projeto, mas ignora bin/obj com .dockerignore
COPY . .

# 🔹 Limpa resíduos locais antes do build
RUN dotnet clean "TrackingCodeAPI.csproj" && rm -rf bin obj

# 🔹 Restaura dependências
RUN dotnet restore "TrackingCodeAPI.csproj"

# 🔹 Compila e publica
RUN dotnet publish "TrackingCodeAPI.csproj" -c Release -o /app/publish

# ---------------------------
# ETAPA 3 - Imagem Final
# ---------------------------
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "TrackingCodeAPI.dll"]
