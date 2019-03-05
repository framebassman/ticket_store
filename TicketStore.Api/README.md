не забыть про libwkhtmltox.so
Пример докерфайла:

```
FROM microsoft/aspnetcore:2.0 AS base

WORKDIR /app

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY MyProj/MyProj.csproj MyProj/
RUN dotnet restore MyProj/MyProj.csproj
COPY . .
WORKDIR /src/MyProj
RUN dotnet build MyProj.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish MyProj.csproj -c Release -o /app

FROM base AS final

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

WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MyProj.dll"]
```