#!/bin/sh

cd ..
docker build . -f Docker/Dockerfile.MVC -t stuckenholz/seminarmanagermvc:1.0
docker login
docker push stuckenholz/seminarmanagermvc:1.0