FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MassTransit.AccountOrchestrator/MassTransit.AccountOrchestrator.csproj", "MassTransit.AccountOrchestrator/"]
RUN dotnet restore "MassTransit.AccountOrchestrator/MassTransit.AccountOrchestrator.csproj"
COPY . .
WORKDIR "/src/MassTransit.AccountOrchestrator"
RUN dotnet build "MassTransit.AccountOrchestrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MassTransit.AccountOrchestrator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MassTransit.AccountOrchestrator.dll"]
