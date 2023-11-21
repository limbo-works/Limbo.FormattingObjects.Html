@echo off
dotnet build src/Limbo.FormattingObjects --configuration Release /t:rebuild /t:pack -p:PackageOutputPath=../../releases/nuget