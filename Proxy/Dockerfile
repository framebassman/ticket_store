FROM traefik:1.7
COPY ./Proxy /app
WORKDIR /app
ENTRYPOINT ["./traefik"]
CMD ["--configfile=./traefik/config/traefik.toml"]
