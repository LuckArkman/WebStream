FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["catalog.API/catalog.API.csproj", "catalog.API/"]
RUN dotnet restore "catalog.API/catalog.API.csproj"
COPY . .
WORKDIR "/src/catalog.API"
RUN dotnet build "catalog.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "catalog.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "catalog.API.dll"]
