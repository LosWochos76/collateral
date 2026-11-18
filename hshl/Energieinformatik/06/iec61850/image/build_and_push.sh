#!/usr/bin/env bash
set -euo pipefail

IMAGE_NAME="stuckenholz/iec61850"
TAG="${1:-latest}"

if [[ ! "$TAG" =~ ^[A-Za-z0-9_][A-Za-z0-9_.-]{0,127}$ ]]; then
  echo "Ungueltiger Docker-Tag: $TAG" >&2
  exit 2
fi

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

# Multi-Plattform-Build: amd64 fuer Windows/Intel-Rechner der Studierenden,
# arm64 fuer Apple Silicon. buildx baut beide Varianten und uebertraegt sie
# als ein gemeinsames Manifest direkt nach Docker Hub.
PLATFORMS="${PLATFORMS:-linux/amd64,linux/arm64}"

echo "Baue und uebertrage ${IMAGE_NAME}:${TAG} fuer ${PLATFORMS}"
docker buildx build --pull --platform "${PLATFORMS}" --tag "${IMAGE_NAME}:${TAG}" --push "${SCRIPT_DIR}"

echo "Fertig: ${IMAGE_NAME}:${TAG}"
