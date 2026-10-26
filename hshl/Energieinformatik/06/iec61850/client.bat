@echo off
setlocal

rem Verzeichnis dieses Skripts ohne abschliessenden Backslash
set "SCRIPT_DIR=%~dp0"
set "SCRIPT_DIR=%SCRIPT_DIR:~0,-1%"
set "IMAGE=stuckenholz/iec61850:latest"
set "NETWORK=iec61850-net"

if "%~1"=="" (
  echo Verwendung: %~nx0 wind ^| switch on^|off 1>&2
  exit /b 1
)

rem Server starten, falls einer der beiden noch nicht laeuft
set "WIND="
set "SWITCH="
for /f %%i in ('docker compose -f "%SCRIPT_DIR%\compose.yaml" --project-directory "%SCRIPT_DIR%" ps -q wind 2^>nul') do set "WIND=%%i"
for /f %%i in ('docker compose -f "%SCRIPT_DIR%\compose.yaml" --project-directory "%SCRIPT_DIR%" ps -q switch 2^>nul') do set "SWITCH=%%i"
if not defined WIND goto starten
if not defined SWITCH goto starten
goto ausfuehren

:starten
docker compose -f "%SCRIPT_DIR%\compose.yaml" --project-directory "%SCRIPT_DIR%" up -d wind switch || exit /b 1

:ausfuehren
docker run --rm --network "%NETWORK%" -v "%SCRIPT_DIR%:/exercise:ro" "%IMAGE%" python client.py %*
