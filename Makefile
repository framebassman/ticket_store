# dev
build-dev:
	docker-compose \
		--project-directory=${PWD} \
		--project-name=ticket_store \
		-f Deploy/docker-compose.yml \
		-f Deploy/docker-compose.development.yml \
		build ${ARGS}

start-dev:
	make grant-permissions-to-cert
	make ngrok
	docker-compose \
		--project-directory=${PWD} \
		--project-name=ticket_store \
		-f Deploy/docker-compose.yml \
		-f Deploy/docker-compose.development.yml \
		up --build

stop-dev:
	docker-compose \
		--project-directory=${PWD} \
		--project-name=ticket_store \
		-f Deploy/docker-compose.yml \
		-f Deploy/docker-compose.development.yml \
		down

# test
start-test:
	make grant-permissions-to-cert
	docker-compose \
		--project-directory=${PWD} \
		--project-name=ticket_store \
		-f Deploy/docker-compose.yml \
		-f Deploy/docker-compose.test.yml \
		up --build -d

stop-test:
	docker-compose \
		--project-directory=${PWD} \
		--project-name=ticket_store \
		-f Deploy/docker-compose.yml \
		-f Deploy/docker-compose.test.yml \
		down

# db only
start-db:
	docker-compose \
		--project-directory=${PWD} \
		--project-name=ticket_store \
		-f Deploy/docker-compose.db.yml \
		up --build -d

stop-db:
	docker-compose \
		--project-directory=${PWD} \
		--project-name=ticket_store \
		-f Deploy/docker-compose.db.yml \
		down

# prod
build-prod:
	docker-compose \
		--project-directory=${PWD} \
		--project-name=ticket_store \
		-f Deploy/docker-compose.yml \
		-f Deploy/docker-compose.production.yml \
		build ${ARGS}

stop-prod:
	docker-compose \
		--project-directory=${PWD} \
		--project-name=ticket_store \
		-f Deploy/docker-compose.yml \
		-f Deploy/docker-compose.production.yml \
		down

start-prod:
	make grant-permissions-to-cert
	docker-compose \
		--project-directory=${PWD} \
		--project-name=ticket_store \
		-f Deploy/docker-compose.yml \
		-f Deploy/docker-compose.production.yml \
		pull
	docker-compose \
		--project-directory=${PWD} \
		--project-name=ticket_store \
		-f Deploy/docker-compose.yml \
		-f Deploy/docker-compose.production.yml \
		up -d

push:
	docker login
	docker-compose \
		--project-directory=${PWD} \
		--project-name=ticket_store \
		-f Deploy/docker-compose.yml \
		-f Deploy/docker-compose.production.yml \
		push

pull:
	docker login
	docker-compose \
		--project-directory=${PWD} \
		--project-name=ticket_store \
		-f Deploy/docker-compose.yml \
		-f Deploy/docker-compose.production.yml \
		pull

# clean dungling images/volumes
docker-cleanup:
	docker volume rm $$(docker volume ls -qf dangling=true)
	docker rmi $$(docker images -a | grep "^<none>" | awk '{print $$3}')
	docker rmi $$(docker images -a --filter=dangling=true -q)
	docker rm $$(docker ps --filter=status=exited --filter=status=created -q)

# connect to db
db-dev:
	docker exec -it postgres psql postgresql://store_user:KqCQzyH2akGB9gQ4@localhost:5432/store_db

db-test:
	docker exec -it postgres psql postgresql://store_user:KqCQzyH2akGB9gQ4@localhost:5432/store_db

db-prod:
	psql postgresql://store_user:GMQCruf5SzsCGR2xd3euUVZQG3c@188.68.210.162:5432/store_db

# run tests
test-api-integration:
	dotnet test Services/TicketStore.Api.Tests \
		--filter Category!=DevelopmentData

test-api-unit:
	dotnet test Services/TicketStore.Api.Tests.Unit

test-web-unit:
	dotnet test Services/TicketStore.Web.Tests.Unit

# migrate db
migrate-dev:
	docker exec store_api dotnet ef database update --verbose

migrate-test:
	export ASPNETCORE_ENVIRONMENT=TestMigrations; \
	dotnet ef database update --project Services/TicketStore.Data/TicketStore.Data.csproj --verbose

# etc
grant-permissions-to-cert:
	chmod 600 ./Services/Proxy/certs/acme.json

ngrok:
	sh ./Scripts/ngrok/launch.sh

db-seed:
	dotnet test Services/TicketStore.Api.Tests \
		--filter Category=DevelopmentData
