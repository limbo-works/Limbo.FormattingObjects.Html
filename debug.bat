@echo off
dotnet build src/Limbo.FormattingObjects.Html --configuration Debug /t:rebuild /t:pack -p:PackageOutputPath=c:/nuget