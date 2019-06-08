FROM microsoft/dotnet:2.2-sdk AS build-env
COPY ./TicketStore.Api/ /app
WORKDIR /app
RUN dotnet clean
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:2.2.0-aspnetcore-runtime
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
        zlib1g \
        fontconfig \
        libfreetype6 \
        libx11-6 \
        libxext6 \
        libxrender1 \
    && curl -o /usr/lib/libwkhtmltox.so \
        --location \
        https://github.com/rdvojmoc/DinkToPdf/raw/v1.0.8/v0.12.4/64%20bit/libwkhtmltox.so
RUN apt-get update \
    && apt-get install -y \
        libgdiplus \
        libc6-dev
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT dotnet TicketStore.Api.dll
