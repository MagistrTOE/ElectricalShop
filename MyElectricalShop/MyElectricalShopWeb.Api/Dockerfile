#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MyElectricalShop/MyElectricalShopWeb.Api/MyElectricalShop.Web.Api.csproj", "MyElectricalShop/MyElectricalShopWeb.Api/"]
COPY ["MyElectricalShop/MyElectricalShop.Application/MyElectricalShop.Application.csproj", "MyElectricalShop/MyElectricalShop.Application/"]
COPY ["MyElectricalShop/MyElectricalShop.Infrastructure/MyElectricalShop.Infrastructure.csproj", "MyElectricalShop/MyElectricalShop.Infrastructure/"]
COPY ["BlackCase/Data.EntityFrameworkCore/Data.EntityFrameworkCore.csproj", "BlackCase/Data.EntityFrameworkCore/"]
COPY ["BlackCase/Data/Data.csproj", "BlackCase/Data/"]
COPY ["BlackCase/Data.Entities/Data.Entities.csproj", "BlackCase/Data.Entities/"]
COPY ["BlackCase/Core/Core.csproj", "BlackCase/Core/"]
COPY ["MyElectricalShop/MyElectricalShop.Domain/MyElectricalShop.Domain.csproj", "MyElectricalShop/MyElectricalShop.Domain/"]
COPY ["Shared/MyElectricalShop.Shared/MyElectricalShop.Shared.ExternalEvents.csproj", "Shared/MyElectricalShop.Shared/"]
RUN dotnet restore "MyElectricalShop/MyElectricalShopWeb.Api/MyElectricalShop.Web.Api.csproj"
COPY . .
WORKDIR "/src/MyElectricalShop/MyElectricalShopWeb.Api"
RUN dotnet build "MyElectricalShop.Web.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyElectricalShop.Web.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyElectricalShop.Web.Api.dll"]