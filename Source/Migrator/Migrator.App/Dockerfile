FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Source/Migrator/Migrator.App/Migrator.App.csproj", "Source/Migrator/Migrator.App/"]
COPY ["Source/Api/ToDo.Api.Data/ToDo.Api.Data.csproj", "Source/Api/ToDo.Api.Data/"]
COPY ["Source/Api/ToDo.Api.Core/ToDo.Api.Core.csproj", "Source/Api/ToDo.Api.Core/"]
RUN dotnet restore "Source/Migrator/Migrator.App/Migrator.App.csproj"
COPY . .
WORKDIR "/src/Source/Migrator/Migrator.App"
RUN dotnet build "Migrator.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Migrator.App.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Migrator.App.dll"]
