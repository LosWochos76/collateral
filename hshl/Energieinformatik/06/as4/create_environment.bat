@echo off
setlocal

set "SCRIPT_DIR=%~dp0"
set "VENV_DIR=%SCRIPT_DIR%.venv"

rem Python-Launcher "py" bevorzugen, sonst "python" aus dem PATH
set "PYTHON=python"
where py >nul 2>nul && set "PYTHON=py -3"

echo Erstelle virtuelle Umgebung in %VENV_DIR%
%PYTHON% -m venv "%VENV_DIR%" || exit /b 1

echo Installiere Abhaengigkeiten aus requirements.txt
"%VENV_DIR%\Scripts\python.exe" -m pip install --upgrade pip || exit /b 1
"%VENV_DIR%\Scripts\python.exe" -m pip install -r "%SCRIPT_DIR%requirements.txt" || exit /b 1

echo Fertig. Umgebung liegt in %VENV_DIR%
