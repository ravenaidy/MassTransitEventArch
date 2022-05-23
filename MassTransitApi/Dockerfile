FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MassTransitApi/MassTransitApi.csproj", "MassTransitApi/"]
RUN dotnet restore "MassTransitApi/MassTransitApi.csproj"
COPY . .
WORKDIR "/src/MassTransitApi"
RUN dotnet build "MassTransitApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MassTransitApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MassTransitApi.dll"]
