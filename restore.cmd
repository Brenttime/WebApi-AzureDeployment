SET VSVER=16.0
REM run 2019 init
call "C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\Common7\Tools\VsDevCmd.bat" -arch=x64
REM run 2019 selfcheck as well
call "C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\Common7\Tools\VsDevCmd.bat" -arch=x64 -test

rem Restore nuget packages
nuget restore %~dp0\WebApi.sln
dotnet restore %~dp0\WebApi.sln --force


