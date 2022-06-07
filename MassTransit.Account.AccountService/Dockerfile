FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MassTransit.Account.AccountService/MassTransit.Account.AccountService.csproj", "MassTransit.Account.AccountService/"]
RUN dotnet restore "MassTransit.Account.AccountService/MassTransit.Account.AccountService.csproj"
COPY . .
WORKDIR "/src/MassTransit.Account.AccountService"
RUN dotnet build "MassTransit.Account.AccountService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MassTransit.Account.AccountService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MassTransit.Account.AccountService.dll"]
