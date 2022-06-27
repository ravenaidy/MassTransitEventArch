FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MassTransit.BFFServices.SignalRWorker/MassTransit.BFFServices.SignalRWorker.csproj", "MassTransit.BFFServices.SignalRWorker/"]
RUN dotnet restore "MassTransit.BFFServices.SignalRWorker/MassTransit.BFFServices.SignalRWorker.csproj"
COPY . .
WORKDIR "/src/MassTransit.BFFServices.SignalRWorker"
RUN dotnet build "MassTransit.BFFServices.SignalRWorker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MassTransit.BFFServices.SignalRWorker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MassTransit.BFFServices.SignalRWorker.dll"]
