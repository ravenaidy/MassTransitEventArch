FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MassTransit.SignalR.SignalRService/MassTransit.SignalR.SignalRService.csproj", "MassTransit.SignalR.SignalRService/"]
RUN dotnet restore "MassTransit.SignalR.SignalRService/MassTransit.SignalR.SignalRService.csproj"
COPY . .
WORKDIR "/src/MassTransit.SignalR.SignalRService"
RUN dotnet build "MassTransit.SignalR.SignalRService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MassTransit.SignalR.SignalRService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MassTransit.SignalR.SignalRService.dll"]
