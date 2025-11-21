#!/usr/bin/env bash

SCRIPT_DIR=$( cd -- "$( dirname -- "${BASH_SOURCE[0]}" )" &> /dev/null && pwd )

PROJECT_FOLDER="$SCRIPT_DIR/../"
PROJECT_FILE=$(find "$PROJECT_FOLDER" -type f -name '*.csproj')

~/.dotnet/dotnet run --project "$PROJECT_FILE"