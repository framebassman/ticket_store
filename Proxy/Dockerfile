FROM traefik:1.7
COPY ./Proxy /app
WORKDIR /app
ENTRYPOINT ["./traefik"]
CMD ["--configfile=/app/traefik/config/traefik.toml"]
