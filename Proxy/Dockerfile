FROM traefik:1.7
COPY ./Proxy .
ENTRYPOINT ["./traefik"]
CMD ["--configfile=./config/traefik.toml"]
