FROM postgres:11.4-alpine
COPY ./Postgres/user.sql /docker-entrypoint-initdb.d/
RUN chmod 0755 /docker-entrypoint-initdb.d/user.sql
CMD ["postgres"]
