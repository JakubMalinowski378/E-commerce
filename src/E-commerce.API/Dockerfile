FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/E-commerce.API/E-commerce.API.csproj", "src/E-commerce.API/"]
COPY ["src/E-commerce.Application/E-commerce.Application.csproj", "src/E-commerce.Application/"]
COPY ["src/E-commerce.Domain/E-commerce.Domain.csproj", "src/E-commerce.Domain/"]
COPY ["src/E-commerce.Infrastructure/E-commerce.Infrastructure.csproj", "src/E-commerce.Infrastructure/"]

COPY ["tests/E-commerce.API.Tests/E-commerce.API.Tests.csproj", "tests/E-commerce.API.Tests/"]
COPY ["tests/E-commerce.Application.Tests/E-commerce.Application.csproj", "tests/E-commerce.Application.Tests/"]
COPY ["tests/E-commerce.Infrastructure.Tests/E-commerce.Infrastructure.Tests.csproj", "tests/E-commerce.Infrastructure.Tests/"]

RUN dotnet restore "./src/E-commerce.API/E-commerce.API.csproj"
COPY . .
WORKDIR "/src/src/E-commerce.API"
RUN dotnet build "./E-commerce.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./E-commerce.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "E-commerce.API.dll"]