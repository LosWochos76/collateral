#!/bin/sh

docker build . -f Docker/mvc/Dockerfile -t stuckenholz/seminarmanagermvc:latest
docker login
docker push stuckenholz/seminarmanagermvc:latest
