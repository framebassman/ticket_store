#!/bin/bash

address=$1
if [[ "$address" == "" ]]
then
  echo "address is not specified"
  exit
fi

attempts=0
res=$(curl -k -s -o /dev/null -w "%{http_code}" $address)
until [ $res -eq "200" ] || [ $res -eq "403" ]; do
    if [ $attempts -ge 20 ]; then
        exit 1
    fi
    res=$(curl -k -s -o /dev/null -w "%{http_code}" $address)
    echo $res
    attempts=$((attempts+1))
    sleep 3
done
echo "I'm ready"
