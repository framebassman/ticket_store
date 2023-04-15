FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TicketStore.Api/TicketStore.Api.csproj", "TicketStore.Api/"]
RUN dotnet restore "TicketStore.Api/TicketStore.Api.csproj"
COPY . .
WORKDIR "/src/TicketStore.Api"
RUN dotnet build "TicketStore.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TicketStore.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TicketStore.Api.dll"]
