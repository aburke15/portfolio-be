# syntax=docker/dockerfile:1 Learn more about the "FROM" Dockerfile command.
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copy csproj, config, and restore packages as distinct layers
COPY *.csproj *.config ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "ABU.Portfolio.dll"]