@echo off
setlocal

rem Verzeichnis dieses Skripts ohne abschliessenden Backslash
set "SCRIPT_DIR=%~dp0"
set "SCRIPT_DIR=%SCRIPT_DIR:~0,-1%"

echo Ziehe stuckenholz/iec61850:latest von Docker Hub
docker compose -f "%SCRIPT_DIR%\compose.yaml" --project-directory "%SCRIPT_DIR%" pull || exit /b 1

echo Fertig. Mit 'client.bat wind' bzw. 'client.bat switch on^|off' ansprechen.
