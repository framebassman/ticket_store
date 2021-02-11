#!/bin/bash
if [[ $CIRCLE_BUILD_NUM != "" ]]; then
  docker-compose \
    --project-directory=${PWD} \
    --project-name=ticket_store \
    -f Deploy/docker-compose.yml \
    -f Deploy/docker-compose.production.yml \
    build ${ARGS}
  echo "$DOCKER_PASSWORD" | docker login -u "$DOCKER_USERNAME" --password-stdin
  docker-compose \
    -f Deploy/docker-compose.yml \
    -f Deploy/docker-compose.production.yml \
    push
fi
