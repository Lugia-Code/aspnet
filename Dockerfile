FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["TrackingCodeAPI.csproj", "./"]

# 🔹 1️⃣ Restaura os pacotes
RUN dotnet restore "TrackingCodeAPI.csproj"

# 🔹 2️⃣ Copia o resto do código
COPY . .

# 🔹 3️⃣ Roda um restore extra só para garantir
RUN dotnet restore "TrackingCodeAPI.csproj"

# 🔹 4️⃣ Limpa build anterior
RUN dotnet clean "TrackingCodeAPI.csproj"

# 🔹 5️⃣ Compila e publica
RUN dotnet publish "TrackingCodeAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "TrackingCodeAPI.dll"]
