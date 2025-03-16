# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Host/EComNetMonolith.WebApiHost/EComNetMonolith.WebApiHost.csproj", "Host/EComNetMonolith.WebApiHost/"]
COPY ["Modules/Cart/EComNetMonolith.Cart/EComNetMonolith.Cart.csproj", "Modules/Cart/EComNetMonolith.Cart/"]
COPY ["Shared/EComNetMonolith.Shared/EComNetMonolith.Shared.csproj", "Shared/EComNetMonolith.Shared/"]
COPY ["Modules/Order/EComNetMonolith.Order/EComNetMonolith.Order.csproj", "Modules/Order/EComNetMonolith.Order/"]
COPY ["Modules/Products/EComNetMonolith.Products/EComNetMonolith.Products.csproj", "Modules/Products/EComNetMonolith.Products/"]
RUN dotnet restore "./Host/EComNetMonolith.WebApiHost/EComNetMonolith.WebApiHost.csproj"
COPY . .
WORKDIR "/src/Host/EComNetMonolith.WebApiHost"
RUN dotnet build "./EComNetMonolith.WebApiHost.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EComNetMonolith.WebApiHost.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EComNetMonolith.WebApiHost.dll"]