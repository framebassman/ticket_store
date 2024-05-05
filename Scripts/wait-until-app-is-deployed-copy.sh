#!/bin/bash

function service_status_code {
  echo $(curl -s -o /dev/null -w "%{http_code}" $host:$1/healthcheck)
}

function wait_service {
  res=$(service_status_code $2)
  until [[ "$res" == "200" ]]; do
    res=$(service_status_code $2)
    echo "$1 status: $res"
    sleep 3
  done
}

apiPort=3000
webPort=4000
fakeSenderPort=5000
dummyPort=5001

host="localhost"

# Wait services
wait_service "api" $apiPort
wait_service "web" $webPort
wait_service "fake_sender" $fakeSenderPort
wait_service "dummy" $dummyPort

echo "I'm ready"
