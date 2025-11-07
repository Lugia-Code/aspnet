FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["TrackingCodeAPI.csproj", "./"]

# 🔹 Restaura dependências
RUN dotnet restore "TrackingCodeAPI.csproj"

# 🔹 Copia o restante do código
COPY . .

# 🔹 Garante restauração completa
RUN dotnet restore "TrackingCodeAPI.csproj"

# 🔹 Limpa qualquer build anterior
RUN rm -rf obj bin

# 🔹 Compila e publica
RUN dotnet publish "TrackingCodeAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "TrackingCodeAPI.dll"]
