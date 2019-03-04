FROM postgres:9.5.14-alpine
COPY ./Postgres/user.sql /docker-entrypoint-initdb.d/
RUN chmod 0755 /docker-entrypoint-initdb.d/user.sql
CMD ["postgres"]
