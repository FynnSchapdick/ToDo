FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Source/App/ToDo.App/ToDo.App.csproj", "Source/App/ToDo.App/"]
COPY ["Source/App/ToDo.App.Core/ToDo.App.Core.csproj", "Source/App/ToDo.App.Core/"]
COPY ["Source/App/ToDo.App.Infrastructure/ToDo.App.Infrastructure.csproj", "Source/App/ToDo.App.Infrastructure/"]
COPY ["Source/Shared/ToDo.Client/ToDo.Client.csproj", "Source/Shared/ToDo.Client/"]
RUN dotnet restore "Source/App/ToDo.App/ToDo.App.csproj"
COPY . .
WORKDIR "/src/Source/App/ToDo.App"
RUN dotnet build "ToDo.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ToDo.App.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ToDo.App.dll"]
