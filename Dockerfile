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

# 🔹 Copia tudo (código + csproj)
COPY . .

# 🔹 Restaura dependências antes de limpar
RUN dotnet restore "TrackingCodeAPI.csproj"

# 🔹 Remove possíveis resíduos de build local
RUN rm -rf bin obj

# 🔹 Compila e publica
RUN dotnet publish "TrackingCodeAPI.csproj" -c Release -o /app/publish

# ---------------------------
# ETAPA 3 - Imagem Final
# ---------------------------
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "TrackingCodeAPI.dll"]
