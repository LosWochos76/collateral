#!/bin/sh
docker build -f Dockerfile-arm64 -t stuckenholz/postgis-pgvector-arm64 .
docker push stuckenholz/postgis-pgvector-arm64:latest

