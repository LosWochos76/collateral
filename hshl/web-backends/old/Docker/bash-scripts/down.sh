#!/bin/sh

docker stop pgadmin
docker rm pgadmin
docker stop postgres
docker rm postgres
docker network rm postgres-network