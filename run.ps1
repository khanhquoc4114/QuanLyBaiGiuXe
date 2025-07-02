# ============================
# Configuration
# ============================
$PROJECT_NAME = "QuanLyBaiGiuXe"
$CONFIGURATION = "Debug"
$OUTPUT_DIR = "$PROJECT_NAME\bin\$CONFIGURATION"
$PYTHON_FOLDER = "python-server"

Write-Host "Checking project structure..."

# ============================
# Build project
# ============================
if (Test-Path "$PROJECT_NAME.sln") {
    Write-Host "Building solution (.sln)..."
    dotnet build "$PROJECT_NAME.sln" --configuration $CONFIGURATION
} elseif (Test-Path "$PROJECT_NAME\$PROJECT_NAME.csproj") {
    Write-Host "Building project (.csproj)..."
    dotnet build "$PROJECT_NAME\$PROJECT_NAME.csproj" --configuration $CONFIGURATION
} else {
    Write-Host "Project file (.sln or .csproj) not found."
    exit 1
}

# ============================
# Verify build success
# ============================
if ($LASTEXITCODE -ne 0) {
    Write-Host "Build failed."
    exit 1
}

# ============================
# Copy python-server folder
# ============================
if (Test-Path $PYTHON_FOLDER) {
    Write-Host "Copying python-server to output directory..."

    # Create output directory if it does not exist
    if (!(Test-Path $OUTPUT_DIR)) {
        New-Item -ItemType Directory -Path $OUTPUT_DIR | Out-Null
    }

    # Copy the entire python-server folder into output directory
    Copy-Item -Recurse -Force $PYTHON_FOLDER "$OUTPUT_DIR\python-server"
    Write-Host "python-server folder copied successfully."
} else {
    Write-Host "python-server folder not found."
    exit 1
}

# ============================
# Run application
# ============================
$exePath = "$OUTPUT_DIR\$PROJECT_NAME.exe"
$dllPath = "$OUTPUT_DIR\$PROJECT_NAME.dll"

Write-Host "Running application..."

if (Test-Path $exePath) {
    & $exePath
} elseif (Test-Path $dllPath) {
    dotnet $dllPath
} else {
    Write-Host "Executable file not found."
    exit 1
}
