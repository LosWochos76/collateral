#!/usr/bin/env bash
set -euo pipefail

IMAGE_NAME="stuckenholz/modbus-server"
TAG="${1:-latest}"

if [[ ! "$TAG" =~ ^[A-Za-z0-9_][A-Za-z0-9_.-]{0,127}$ ]]; then
  echo "Ungueltiger Docker-Tag: $TAG" >&2
  exit 2
fi

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

echo "Baue ${IMAGE_NAME}:${TAG}"
docker build --pull --tag "${IMAGE_NAME}:${TAG}" "${SCRIPT_DIR}"

echo "Uebertrage ${IMAGE_NAME}:${TAG} nach Docker Hub"
docker push "${IMAGE_NAME}:${TAG}"

echo "Fertig: ${IMAGE_NAME}:${TAG}"
