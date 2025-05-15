#!/bin/sh
docker build -f Dockerfile-amd64 -t stuckenholz/postgis-pgvector-amd64 .
docker push stuckenholz/postgis-pgvector-amd64:latest
