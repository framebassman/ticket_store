FROM postgres:11.11-alpine
COPY ./Services/Postgres/user.sql /docker-entrypoint-initdb.d/
RUN chmod 0755 /docker-entrypoint-initdb.d/user.sql
CMD ["postgres"]
