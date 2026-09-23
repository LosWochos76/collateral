#!/usr/bin/env bash
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

echo "Ziehe stuckenholz/iec61850:latest von Docker Hub"
docker compose -f "${SCRIPT_DIR}/compose.yaml" --project-directory "${SCRIPT_DIR}" pull

echo "Fertig. Mit './client.sh wind' bzw. './client.sh switch on|off' ansprechen."
