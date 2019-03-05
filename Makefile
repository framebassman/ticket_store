build:
	docker-compose \
		--project-directory=${PWD} \
		--project-name=ticket_store \
		-f Deploy/docker-compose.development.yml \
		build ${ARGS}

start:
	docker-compose \
		--project-directory=${PWD} \
		--project-name=ticket_store \
		-f Deploy/docker-compose.development.yml \
		up --build

stop-dev:
	docker-compose \
		--project-directory=${PWD} \
		--project-name=ticket_store \
		-f Deploy/docker-compose.development.yml \
		down

# clean dungling images/volumes
docker-cleanup:
	docker volume rm $$(docker volume ls -qf dangling=true)
	docker rmi $$(docker images -a | grep "^<none>" | awk '{print $$3}')
	docker rmi $$(docker images -a --filter=dangling=true -q)
	docker rm $$(docker ps --filter=status=exited --filter=status=created -q)

db-dev:
	docker exec -it postgres psql postgresql://store:W6TTT3WY4Nzqpj5z@localhost:5432/store

db-prod:
	docker exec -it postgres \
	mkdir .postgresql && \
	wget "https://storage.yandexcloud.net/cloud-certs/CA.pem" -O ~/.postgresql/root.crt && \
	chmod 0600 ~/.postgresql/root.crt \
	psql "host=rc1b-72blhko8vjza9fgp.mdb.yandexcloud.net \
      port=6432 \
      sslmode=verify-full \
      dbname=store \
      user=store \
      target_session_attrs=read-write"
