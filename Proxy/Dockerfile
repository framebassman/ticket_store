FROM traefik:2.0
COPY ./Proxy .
ENTRYPOINT ["./traefik"]
CMD ["--configfile=./config/traefik.toml"]
