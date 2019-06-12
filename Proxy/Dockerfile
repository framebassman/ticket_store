FROM traefik:2.0
COPY ./Proxy .
ENTRYPOINT ["./traefik"]
CMD ["--configfile=./traefik/config/traefik.toml"]
