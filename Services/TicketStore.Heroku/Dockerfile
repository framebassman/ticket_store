FROM mcr.microsoft.com/dotnet/core/sdk:3.1.201-alpine3.11 AS build-env
COPY . /app
WORKDIR /app
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.3-alpine3.11
WORKDIR /app
COPY --from=build-env /app/out .
ENV ASPNETCORE_URLS=http://localhost:$PORT
ENV ASPNETCORE_ENVIRONMENT=Staging
ENTRYPOINT dotnet TicketStore.Heroku.dll
