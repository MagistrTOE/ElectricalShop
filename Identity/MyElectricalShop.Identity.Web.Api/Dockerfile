#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Identity/MyElectricalShop.Identity.Web.Api/MyElectricalShop.Identity.Web.Api.csproj", "Identity/MyElectricalShop.Identity.Web.Api/"]
COPY ["Identity/MyElectricalShop.Identity.Infrastructure/MyElectricalShop.Identity.Infrastructure.csproj", "Identity/MyElectricalShop.Identity.Infrastructure/"]
COPY ["Identity/MyElectricalShop.Identity.Domain/MyElectricalShop.Identity.Domain.csproj", "Identity/MyElectricalShop.Identity.Domain/"]
COPY ["Identity/MyElectricalShop.Identity.Application/MyElectricalShop.Identity.Application.csproj", "Identity/MyElectricalShop.Identity.Application/"]
COPY ["BlackCase/Core/Core.csproj", "BlackCase/Core/"]
COPY ["Shared/MyElectricalShop.Shared/MyElectricalShop.Shared.ExternalEvents.csproj", "Shared/MyElectricalShop.Shared/"]
RUN dotnet restore "Identity/MyElectricalShop.Identity.Web.Api/MyElectricalShop.Identity.Web.Api.csproj"
COPY . .
WORKDIR "/src/Identity/MyElectricalShop.Identity.Web.Api"
RUN dotnet build "MyElectricalShop.Identity.Web.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyElectricalShop.Identity.Web.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyElectricalShop.Identity.Web.Api.dll"]