#!/bin/bash

PROJECT_NAME="QuanLyBaiGiuXe"
CONFIGURATION="Debug"

cd "$PROJECT_NAME" || { echo "❌ Project directory not found!"; exit 1; }

if [ -f "$PROJECT_NAME.sln" ]; then
    echo "📦 Building solution $PROJECT_NAME.sln..."
    dotnet build "$PROJECT_NAME.sln" --configuration $CONFIGURATION
else
    echo "📦 Building project $PROJECT_NAME.csproj..."
    dotnet build "$PROJECT_NAME/$PROJECT_NAME.csproj" --configuration $CONFIGURATION
fi

if [ $? -ne 0 ]; then
    echo "❌ Build failed."
    exit 1
fi

echo "📁 Copying python-server to output directory..."
cp -r python-server "$PROJECT_NAME/bin/$CONFIGURATION/"

echo "🚀 Running $PROJECT_NAME..."
dotnet "$PROJECT_NAME/bin/$CONFIGURATION/$PROJECT_NAME.dll"
