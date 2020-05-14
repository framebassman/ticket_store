FROM mcr.microsoft.com/dotnet/core/sdk:3.1.201-alpine3.11
COPY . /tests
RUN dotnet tool install -g trx2junit
WORKDIR /tests
ENTRYPOINT ["/bin/sh", "dotnet test --filter Category=Dummy"]
CMD ~/.dotnet/tools/trx2junit TestResults/report.trx
