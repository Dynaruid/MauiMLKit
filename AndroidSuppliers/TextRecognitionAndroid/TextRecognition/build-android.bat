@echo off

setlocal enabledelayedexpansion
:: 인자로 받은 경로를 변수에 설정
set "sdkPath=%~1"

:: 앞뒤 공백 제거
for /f "tokens=* delims= " %%x in ("!sdkPath!") do set "sdkPath=%%x"

:: 경로의 모든 백슬래시를 두 개의 백슬래시로 이스케이프 처리
set "sdkPath=!sdkPath:\=\\!"

cd ..\MLKitTextRecognitionAdapter

:: Create local.properties file with the escaped Android SDK path
echo sdk.dir=!sdkPath!> local.properties
.\gradlew.bat %2
