#!/usr/bin/env bash
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
VENV_DIR="${SCRIPT_DIR}/.venv"

echo "Erstelle virtuelle Umgebung in ${VENV_DIR}"
python3 -m venv "${VENV_DIR}"

echo "Installiere Abhaengigkeiten aus requirements.txt"
"${VENV_DIR}/bin/pip" install --upgrade pip
"${VENV_DIR}/bin/pip" install -r "${SCRIPT_DIR}/requirements.txt"

echo "Fertig. Umgebung liegt in ${VENV_DIR}"
