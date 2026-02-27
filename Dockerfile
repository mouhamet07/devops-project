# ----- Build stage -----
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
# Copier le fichier csproj 
COPY gestionStock.csproj ./
RUN dotnet restore
# Copier le reste du code
COPY . ./
# Publier l'application
RUN dotnet publish -c Release -o /app/publish
# ----- Runtime stage -----
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "gestionStock.dll"]