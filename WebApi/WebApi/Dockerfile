FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["WebApi/WebApi/WebApi.csproj", "WebApi/WebApi/"]
RUN dotnet restore "WebApi/WebApi/WebApi.csproj"
COPY . .
WORKDIR "/src/WebApi/WebApi"
RUN dotnet build "WebApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "WebApi.csproj" -c Release -o /app
