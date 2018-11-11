#!/usr/bin/env bash

ApiKey=$1

#exit if any command fails
set -e

dotnet pack -o ../../dist/nuget

dotnet nuget push ./dist/nuget/IPaygateService.*.nupkg -k $ApiKey -s https://api.nuget.org/v3/index.json