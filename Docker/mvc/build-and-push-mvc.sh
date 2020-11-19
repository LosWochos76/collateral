#!/bin/sh

cd ..
docker build . -f Docker/Dockerfile.MVC -t stuckenholz/seminarmanagermvc:latest
docker login
docker push stuckenholz/seminarmanagermvc:latest