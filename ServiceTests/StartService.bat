@echo off
setlocal EnableDelayedExpansion

set "current_dir=%CD%"
cd /d "%~dp0"
cd EM_ServiceEnabler

echo Waiting for service up..
start cmd /c node server.js

timeout /t 5 /nobreak >nul
echo Waiting 5 sec for test set up..
cd ..
cd ServiceCollections_V1

start /wait cmd /c newman run EMSmokeServices.json -d TestData.csv -n 2 --delay-request 500  -r htmlextra --reporter-htmlextra-noSyntaxHighlighting --reporter-htmlextra-omitHeaders --reporter-htmlextra-browserTitle "Employee Management Smoke Suite" --reporter-htmlextra-title "Employee Management Service Test"

echo Waiting 5 seconds for service and test closure
timeout /t 5 /nobreak >nul
taskkill /f /im cmd.exe
