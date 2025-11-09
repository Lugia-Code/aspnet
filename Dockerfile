FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80
ENV AZURE_SQL_CONNECTION_STRING=${AZURE_SQL_CONNECTION_STRING}

RUN apt-get update && \
    apt-get install -y libaio1 && \
    rm -rf /var/lib/apt/lists/*

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["TrackingCodeAPI.csproj", "./"]
RUN dotnet restore "./TrackingCodeAPI.csproj"

COPY . .
RUN dotnet build "./TrackingCodeAPI.csproj" -c "$BUILD_CONFIGURATION" -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TrackingCodeAPI.csproj" -c "$BUILD_CONFIGURATION" -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .

RUN groupadd -g 1001 appuser && \
    useradd -m -u 1001 -g appuser appuser && \
    chown -R appuser:appuser /app
USER appuser

ENTRYPOINT ["dotnet", "TrackingCodeAPI.dll"]
