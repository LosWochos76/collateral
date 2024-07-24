#!/bin/sh

docker container stop wordpress
docker container rm wordpress
docker container stop mariadb
docker container rm mariadb
docker network rm wordpress-network