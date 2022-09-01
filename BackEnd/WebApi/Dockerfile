# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

# Copy csproj and restore as distinct layers
WORKDIR /app
COPY BackEnd/*.sln .
COPY BackEnd/WebApi/*.csproj ./WebApi/
COPY BackEnd/Core/*.csproj ./Core/
COPY BackEnd/DataAccess/*.csproj ./DataAccess/
COPY BackEnd/Application/*.csproj ./Application/
COPY BackEnd/IdentityServer/*.csproj ./IdentityServer/

RUN dotnet restore
    
# Copy everything else and build
COPY BackEnd/WebApi/. ./WebApi/
COPY BackEnd/Core/. ./Core/
COPY BackEnd/DataAccess/. ./DataAccess/
COPY BackEnd/Application/. ./Application/

WORKDIR /app/WebApi
RUN dotnet publish -c Release -o out
    
# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
EXPOSE 80
COPY --from=build-env /app/WebApi/out ./
ENTRYPOINT ["dotnet", "WebApi.dll"]