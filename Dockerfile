# Etapa base: imagem leve para execução
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia apenas o arquivo de projeto para restaurar dependências
COPY ["TrackingCodeAPI.csproj", "./"]
RUN dotnet restore "TrackingCodeAPI.csproj"

# Copia o restante do código
COPY . .

# ⚠️ Limpa builds anteriores (evita duplicações de AssemblyInfo)
RUN dotnet clean "TrackingCodeAPI.csproj"

# Publica em modo Release
RUN dotnet publish "TrackingCodeAPI.csproj" -c Release -o /app/publish

# Etapa final
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "TrackingCodeAPI.dll"]
