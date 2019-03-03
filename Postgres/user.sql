DO
$do$
BEGIN
   IF NOT EXISTS (
      SELECT
      FROM   pg_catalog.pg_roles
      WHERE  rolname = 'tickets_user') THEN

      CREATE ROLE tickets_user LOGIN PASSWORD 'rfSbZWvT9RwLU2dVAw9z';
      GRANT SELECT ON ALL TABLES IN SCHEMA public TO tickets_user;
   END IF;
END
$do$;

CREATE EXTENSION pgcrypto;
