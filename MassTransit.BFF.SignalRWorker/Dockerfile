#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MassTransit.BFF.SignalRWorker/MassTransit.BFF.SignalRWorker.csproj", "MassTransit.BFF.SignalRWorker/"]
RUN dotnet restore "MassTransit.BFF.SignalRWorker/MassTransit.BFF.SignalRWorker.csproj"
COPY . .
WORKDIR "/src/MassTransit.BFF.SignalRWorker"
RUN dotnet build "MassTransit.BFF.SignalRWorker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MassTransit.BFF.SignalRWorker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MassTransit.BFF.SignalRWorker.dll"]