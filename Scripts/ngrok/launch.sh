#!/bin/bash

function docker_host()
{
    docker_host=$DOCKER_HOST
    if [ "$docker_host" = '' ]; then
        docker_host="localhost"
    fi
    host=$(echo $docker_host | sed 's/tcp:\/\///' | sed -e 's/:[0-9][0-9][0-9][0-9]//g')
    echo $host
}

# install ngrok if not found
if [ "$(command -v ngrok)" = '' ]; then
    /bin/bash ${0%/*}/install.sh
fi

echo "Killing ngrok processes (ignore err)"
servers=$(ps ax | grep "ngrok http" | awk '{print $1}')
kill -9 $servers

# is ngrok auth?
if [ "$(cat ~/.ngrok2/ngrok.yml | grep authtoken)" = '' ]; then
    echo "INFO: Authentication required. Please, type authtoken for ngrok"
    read -s authtoken
    ngrok authtoken $authtoken
fi

echo "Starting ngrok..."
ngrok http $(docker_host):80 --bind-tls true --region eu -log=stdout > /dev/null &

echo "Waiting server to connect..."
res=$(curl -s -o /dev/null -w "%{http_code}" localhost:4040/api/tunnels)
until [ $res -ne "000" ]; do
    res=$(curl -s -o /dev/null -w "%{http_code}" localhost:4040/api/tunnels)
done
echo "Ngrok created"

echo "Waiting for api correct answer"
info=$(curl -sb -H "Accept: application/json" localhost:4040/api/tunnels |
    perl -pe 's|.*"public_url":"(https:\/\/.*?)".*$|\1|g')
until [ $info != "{\"tunnels\":[],\"uri\":\"/api/tunnels\"}" ]; do
    info=$(curl -sb -H "Accept: application/json" localhost:4040/api/tunnels |
    perl -pe 's|.*"public_url":"(https:\/\/.*?)".*$|\1|g')
done

ngrok_address=$(curl -sb -H "Accept: application/json" localhost:4040/api/tunnels |
    perl -pe 's|.*"public_url":"(https:\/\/.*?)".*$|\1|g')
echo "\033[32mNgrok link: $ngrok_address\033[m"
