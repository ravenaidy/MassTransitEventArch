FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Masstransit.Api.Auth/Masstransit.Api.Auth.csproj", "Masstransit.Api.Auth/"]
RUN dotnet restore "Masstransit.Api.Auth/Masstransit.Api.Auth.csproj"
COPY . .
WORKDIR "/src/Masstransit.Api.Auth"
RUN dotnet build "Masstransit.Api.Auth.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Masstransit.Api.Auth.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Masstransit.Api.Auth.dll"]
