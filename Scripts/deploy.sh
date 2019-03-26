#!/bin/bash
if [[ $TRAVIS_BUILD_NUMBER != "" ]]; then
  docker-compose \
    --project-directory=${PWD} \
    --project-name=ticket_store \
    -f Deploy/docker-compose.production.yml \
    build ${ARGS}
  echo "$DOCKER_PASSWORD" | docker login -u "$DOCKER_USERNAME" --password-stdin
  docker-compose -f Deploy/docker-compose.production.yml push
fi
