# STAGE 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar solo los archivos de proyecto para restaurar dependencias (esto optimiza el cache de Docker)
COPY ["src/InstagramAnalyzer.WebUI/InstagramAnalyzer.WebUI.csproj", "src/InstagramAnalyzer.WebUI/"]
COPY ["src/InstagramAnalyzer.Infrastructure/InstagramAnalyzer.Infrastructure.csproj", "src/InstagramAnalyzer.Infrastructure/"]
COPY ["src/InstagramAnalyzer.Application/InstagramAnalyzer.Application.csproj", "src/InstagramAnalyzer.Application/"]
COPY ["src/InstagramAnalyzer.Domain/InstagramAnalyzer.Domain.csproj", "src/InstagramAnalyzer.Domain/"]

# Restaurar dependencias
RUN dotnet restore "src/InstagramAnalyzer.WebUI/InstagramAnalyzer.WebUI.csproj"

# Copiar el resto del código y compilar
COPY . .
WORKDIR "/src/src/InstagramAnalyzer.WebUI"
RUN dotnet build "InstagramAnalyzer.WebUI.csproj" -c Release -o /app/build

# STAGE 2: Publish
FROM build AS publish
RUN dotnet publish "InstagramAnalyzer.WebUI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# STAGE 3: Runtime final (imagen ligera)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Exponer el puerto que usa la app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "InstagramAnalyzer.WebUI.dll"]