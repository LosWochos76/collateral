#!/bin/sh

docker network create wordpress-network

docker container run -d \
    --name mariadb \
    --network  wordpress-network \
    -e MARIADB_ROOT_PASSWORD=secret \
    -e MARIADB_USER=wordpress \
    -e MARIADB_PASSWORD=secret \
    -e MARIADB_DATABASE=wordpress \
    mariadb:latest

docker container run -d \
    --name wordpress \
    --network  wordpress-network \
    -e WORDPRESS_DB_HOST=mariadb \
    -e WORDPRESS_DB_USER=wordpress \
    -e WORDPRESS_DB_PASSWORD=secret \
    -e WORDPRESS_DB_NAME=wordpress \
    -p 80:80 \
    wordpress:latest
