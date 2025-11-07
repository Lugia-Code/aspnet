# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o arquivo .csproj e restaura dependências
COPY *.csproj ./
RUN dotnet restore

# Copia o restante dos arquivos e compila
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copia os arquivos publicados da etapa anterior
COPY --from=build /app/publish .

# Define o ponto de entrada
ENTRYPOINT ["dotnet", "TrackingCodeAPI.dll"]
