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
	docker-compose -f Deploy/docker-compose.production.yml push

pull:
	docker login
	docker-compose -f Deploy/docker-compose.production.yml pull

# clean dungling images/volumes
docker-cleanup:
	docker volume rm $$(docker volume ls -qf dangling=true)
	docker rmi $$(docker images -a | grep "^<none>" | awk '{print $$3}')
	docker rmi $$(docker images -a --filter=dangling=true -q)
	docker rm $$(docker ps --filter=status=exited --filter=status=created -q)

# connect to db
db-dev:
	docker exec -it postgres psql postgresql://store_user:KqCQzyH2akGB9gQ4@localhost:5432/store_db

db-init-dev:
	docker exec -it postgres psql postgresql://store_user:KqCQzyH2akGB9gQ4@localhost:5432/store_db \
	-c "INSERT INTO merchants (id, yandex_money_account, place) VALUES (2, 123456789, 'Cherdak')"

	docker exec -it postgres psql postgresql://store_user:KqCQzyH2akGB9gQ4@localhost:5432/store_db \
	-c "INSERT INTO events (id, artist, roubles, press_release, time, poster_url, merchant_id) VALUES (2, 'Muse', 100, 'Nice band', 'Tue, 9 Jul 2019 17:00:00Z', 'https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png', 2)"

db-view-dev:
	docker exec -it postgres psql postgresql://store_user:KqCQzyH2akGB9gQ4@localhost:5432/store_db \
	-c "SELECT * FROM merchants;"

	docker exec -it postgres psql postgresql://store_user:KqCQzyH2akGB9gQ4@localhost:5432/store_db \
	-c "SELECT * FROM events;"

db-test:
	docker exec -it postgres psql postgresql://store_user:KqCQzyH2akGB9gQ4@localhost:5432/store_db

db-prod:
	psql postgresql://store_user:GMQCruf5SzsCGR2xd3euUVZQG3c@188.68.210.162:5432/store_db

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
