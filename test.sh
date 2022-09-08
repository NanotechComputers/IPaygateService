#!/usr/bin/env bash

#exit if any command fails
set -e

dotnet test ./test/Paygate.UnitTests -c Release -f netcoreapp3.1
