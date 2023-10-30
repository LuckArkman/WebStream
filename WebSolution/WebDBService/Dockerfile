FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["WebDBService/WebDBService.csproj", "WebDBService/"]
RUN dotnet restore "WebDBService/WebDBService.csproj"
COPY . .
WORKDIR "/src/WebDBService"
RUN dotnet build "WebDBService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebDBService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebDBService.dll"]
