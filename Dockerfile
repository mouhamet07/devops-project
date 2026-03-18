# ----- Build stage -----
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copier tout le dossier projet
COPY gestionStock/gestionStock ./gestionStock

# Se placer dans le bon dossier
WORKDIR /src/gestionStock

# Restore + build
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# ----- Runtime stage -----
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "gestionStock.dll"]