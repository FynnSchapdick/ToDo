FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Source/Api/ToDo.Api/ToDo.Api.csproj", "Source/Api/ToDo.Api/"]
COPY ["Source/Api/ToDo.Api.Core/ToDo.Api.Core.csproj", "Source/Api/ToDo.Api.Core/"]
COPY ["Source/Api/ToDo.Api.Data/ToDo.Api.Data.csproj", "Source/Api/ToDo.Api.Data/"]
RUN dotnet restore "Source/Api/ToDo.Api/ToDo.Api.csproj"
COPY . .
WORKDIR "/src/Source/Api/ToDo.Api"
RUN dotnet build "ToDo.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ToDo.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ToDo.Api.dll"]
