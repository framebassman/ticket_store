FROM mcr.microsoft.com/dotnet/core/sdk:3.1.201-alpine3.11 AS build-env

ARG REACT_APP_ENVIRONMENT=Production
ARG ASPNETCORE_ENVIRONMENT=Staging 

RUN apk add --update 'nodejs=12.15.0-r1' 'npm=12.15.0-r1'

COPY ./TicketStore.Web /app
COPY ./TicketStore.Data/ /TicketStore.Data

WORKDIR /app/Client
RUN echo REACT_APP_ENVIRONMENT=$REACT_APP_ENVIRONMENT > .env && cat .env

WORKDIR /app
RUN dotnet restore
RUN dotnet-ef database update --project /TicketStore.Data/TicketStore.Data.csproj --verbose
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.3-alpine3.11
WORKDIR /app
COPY --from=build-env /app/out .
ENV ASPNETCORE_ENVIRONMENT=Staging
ENTRYPOINT ["dotnet", "TicketStore.Web.dll"]
