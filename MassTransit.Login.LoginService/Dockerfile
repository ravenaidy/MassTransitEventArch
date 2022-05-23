FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MassTransit.Login.LoginService/MassTransit.Login.LoginService.csproj", "MassTransit.Login.LoginService/"]
RUN dotnet restore "MassTransit.Login.LoginService/MassTransit.Login.LoginService.csproj"
COPY . .
WORKDIR "/src/MassTransit.Login.LoginService"
RUN dotnet build "MassTransit.Login.LoginService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MassTransit.Login.LoginService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MassTransit.Login.LoginService.dll"]
