#!/usr/bin/env bash
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
COMPOSE=(docker compose -f "${SCRIPT_DIR}/compose.yaml" --project-directory "${SCRIPT_DIR}")
IMAGE="stuckenholz/iec61850:latest"
NETWORK="iec61850-net"

if [ "$#" -eq 0 ]; then
  echo "Verwendung: $(basename "$0") wind | switch on|off" >&2
  exit 1
fi

if [ -z "$("${COMPOSE[@]}" ps -q wind 2>/dev/null)" ] || [ -z "$("${COMPOSE[@]}" ps -q switch 2>/dev/null)" ]; then
  "${COMPOSE[@]}" up -d wind switch
fi

docker run --rm \
  --network "${NETWORK}" \
  -v "${SCRIPT_DIR}:/exercise:ro" \
  "${IMAGE}" \
  python client.py "$@"
