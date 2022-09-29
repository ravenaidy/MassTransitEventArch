FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MassTransit.Orchestrator/MassTransit.Orchestrator.csproj", "MassTransit.Orchestrator/"]
RUN dotnet restore "MassTransit.Orchestrator/MassTransit.Orchestrator.csproj"
COPY . .
WORKDIR "/src/MassTransit.Orchestrator"
RUN dotnet build "MassTransit.Orchestrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MassTransit.Orchestrator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MassTransit.Orchestrator.dll"]
