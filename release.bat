@echo off
dotnet build src/Limbo.FormattingObjects.Html --configuration Release /t:rebuild /t:pack -p:PackageOutputPath=../../releases/nuget