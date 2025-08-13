FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ../CatalogoCursos.Domain/CatalogoCursos.Domain.csproj CatalogoCursos.Domain/
COPY ../CatalogoCursos.Application/CatalogoCursos.Application.csproj CatalogoCursos.Application/
COPY ../CatalogoCursos.Infrastructure/CatalogoCursos.Infrastructure.csproj CatalogoCursos.Infrastructure/
COPY CatalogoCursos.API.csproj CatalogoCursos.API/
RUN dotnet restore CatalogoCursos.API/CatalogoCursos.API.csproj
COPY .. .
WORKDIR /src/CatalogoCursos.API
RUN dotnet publish CatalogoCursos.API.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CatalogoCursos.API.dll"]
