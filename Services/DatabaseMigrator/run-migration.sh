#!/bin/sh

export ASPNETCORE_ENVIRONMENT=Test;
~/.dotnet/tools/dotnet-ef database update --project /TicketStore.Data/TicketStore.Data.csproj --verbose
