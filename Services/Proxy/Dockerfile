FROM traefik:1.7
COPY ./Services/Proxy .
ENTRYPOINT ["./traefik"]
CMD ["--configfile=./config/traefik.toml"]
