@echo off
dotnet build src/Limbo.FormattingObjects --configuration Debug /t:rebuild /t:pack -p:PackageOutputPath=c:/nuget