# =========================
# BASE RUNTIME IMAGE
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Production

RUN apt-get update && \
    apt-get install -y libaio1 && \
    rm -rf /var/lib/apt/lists/*

# =========================
# BUILD STAGE
# =========================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["TrackingCodeAPI.csproj", "./"]
RUN dotnet restore "./TrackingCodeAPI.csproj"
COPY . .
RUN dotnet build "./TrackingCodeAPI.csproj" -c Release -o /app/build

# =========================
# PUBLISH STAGE
# =========================
FROM build AS publish
RUN dotnet publish "./TrackingCodeAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# =========================
# FINAL IMAGE
# =========================
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Azure injeta variáveis no runtime, então não definir ENV aqui
# Evita perder acesso às variáveis do App Service
ENTRYPOINT ["dotnet", "TrackingCodeAPI.dll"]
