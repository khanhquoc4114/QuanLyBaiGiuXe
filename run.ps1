# move to the script directory
Set-Location -Path $PSScriptRoot

# Configuration
$PROJECT_NAME = "QuanLyBaiGiuXe"
$CONFIGURATION = "Debug"
$PROJECT_DIR = ".\$PROJECT_NAME"
$OUTPUT_DIR = "$PROJECT_DIR\bin\$CONFIGURATION"
$PYTHON_FOLDER = "python-server"

Write-Host "`n=== Cleaning build ==="
Remove-Item -Recurse -Force "$PROJECT_DIR/bin" -ErrorAction SilentlyContinue
Remove-Item -Recurse -Force "$PROJECT_DIR/obj" -ErrorAction SilentlyContinue

Write-Host "`n=== Building project: $PROJECT_NAME ==="

if (Test-Path "$PROJECT_DIR\$PROJECT_NAME.sln") {
    dotnet build "$PROJECT_DIR\$PROJECT_NAME.sln" --configuration $CONFIGURATION
}
elseif (Test-Path "$PROJECT_DIR\$PROJECT_NAME.csproj") {
    dotnet build "$PROJECT_DIR\$PROJECT_NAME.csproj" --configuration $CONFIGURATION
}
else {
    Write-Error "No .sln or .csproj found in $PROJECT_DIR"
    exit 1
}

if ($LASTEXITCODE -ne 0) {
    Write-Error "Build failed."
    exit 1
}

Write-Host "`n=== Copying python-server ==="

$destPath = "$OUTPUT_DIR\python-server"
if (Test-Path $destPath) {
    Remove-Item -Recurse -Force $destPath
}
Copy-Item -Recurse -Force $PYTHON_FOLDER $destPath
Write-Host "Copy complete to $destPath"

Write-Host "`n=== Running application ==="

$exePath = "$OUTPUT_DIR\$PROJECT_NAME.exe"
$dllPath = "$OUTPUT_DIR\$PROJECT_NAME.dll"

if (Test-Path $exePath) {
    & $exePath
}
elseif (Test-Path $dllPath) {
    dotnet $dllPath
}
else {
    Write-Error "Executable not found: $exePath or $dllPath"
    exit 1
}

Read-Host -Prompt "Press Enter to exit"
