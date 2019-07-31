#!/bin/bash

function url()
{
    if [[ "$OSTYPE" == "darwin"* ]]; then
        echo 'https://bin.equinox.io/c/4VmDzA7iaHb/ngrok-stable-darwin-amd64.zip'
    elif [[ "$OSTYPE" == "linux-gnu"* ]]; then
        echo 'https://bin.equinox.io/c/4VmDzA7iaHb/ngrok-stable-linux-amd64.zip'
    else
        echo 'ERROR: Cannot install ngrok on your machine. Please, instsall ngrok manually and try again'
        exit 1;
    fi
}

# Check unzip
if [ "$(command -v unzip)" = '' ]; then
    echo 'ERROR: Please, install unzip and try again'
    exit 1;
fi

# Read password for sudo rights
echo "INFO: Installing ngrok, this will require sudo. Please, type password:"
read -s pass

zip_path=${0%/*}/ngrok.zip

# Download ngrok archive
curl -o $zip_path $(url)

# Unpack to /usr/local/bin directory
echo $pass | sudo -S unzip -d /usr/local/bin $zip_path > /dev/null 2>&1
echo $pass | sudo -S chown $USER /usr/local/bin/ngrok
rm $zip_path
