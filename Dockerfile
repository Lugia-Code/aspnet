FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Production

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["TrackingCodeAPI.csproj", "./"]
RUN dotnet restore "./TrackingCodeAPI.csproj"
COPY . .
RUN dotnet build "./TrackingCodeAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./TrackingCodeAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TrackingCodeAPI.dll"]
