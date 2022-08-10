FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MassTransit.Chat.MessagesService/MassTransit.Chat.MessagesService.csproj", "MassTransit.Chat.MessagesService/"]
RUN dotnet restore "MassTransit.Chat.MessagesService/MassTransit.Chat.MessagesService.csproj"
COPY . .
WORKDIR "/src/MassTransit.Chat.MessagesService"
RUN dotnet build "MassTransit.Chat.MessagesService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MassTransit.Chat.MessagesService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MassTransit.Chat.MessagesService.dll"]
