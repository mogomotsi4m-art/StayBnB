$ErrorActionPreference = "Stop"
Write-Host "Staybnb local setup" -ForegroundColor Cyan
$version = dotnet --version
Write-Host "Using .NET SDK $version"
if (!(Test-Path ".\Staybnb.csproj")) { throw "Run this script from the repository root containing Staybnb.csproj." }
Write-Host "Checking SQL Server LocalDB..." -ForegroundColor Cyan
try { sqllocaldb info MSSQLLocalDB | Out-Null } catch { Write-Warning "Could not query LocalDB. Confirm SQL Server Express LocalDB is installed and the MSSQLLocalDB instance exists." }
Write-Host "1/5 Restoring NuGet packages..." -ForegroundColor Cyan
dotnet restore
if ($LASTEXITCODE -ne 0) { throw "dotnet restore failed." }
Write-Host "2/5 Restoring local dotnet-ef tool..." -ForegroundColor Cyan
dotnet tool restore
if ($LASTEXITCODE -ne 0) { dotnet tool install --global dotnet-ef --version 10.0.0 }
Write-Host "3/5 Configure development SuperAdmin credentials using user-secrets." -ForegroundColor Cyan
if ((Get-Content .\Staybnb.csproj -Raw) -notmatch "UserSecretsId") { dotnet user-secrets init; if ($LASTEXITCODE -ne 0) { throw "Could not initialize user-secrets." } }
$email = Read-Host "SuperAdmin email (e.g. superadmin@staybnb.local)"
if ([string]::IsNullOrWhiteSpace($email)) { $email = "superadmin@staybnb.local" }
$secure = Read-Host "Enter a strong development-only SuperAdmin password" -AsSecureString
$ptr = [Runtime.InteropServices.Marshal]::SecureStringToBSTR($secure)
try {
  $password = [Runtime.InteropServices.Marshal]::PtrToStringBSTR($ptr)
  dotnet user-secrets set "SuperAdmin:Email" $email
  if ($LASTEXITCODE -ne 0) { throw "Could not save SuperAdmin email." }
  dotnet user-secrets set "SuperAdmin:Password" $password
  if ($LASTEXITCODE -ne 0) { throw "Could not save SuperAdmin password." }
} finally { [Runtime.InteropServices.Marshal]::ZeroFreeBSTR($ptr); $password = $null; $secure.Dispose() }
Write-Host "4/5 Creating/applying the initial EF Core migration..." -ForegroundColor Cyan
if (!(Test-Path ".\Migrations")) { dotnet ef migrations add InitialCreate }
if ($LASTEXITCODE -ne 0) { throw "Migration generation failed. Read the preceding error before continuing." }
dotnet ef database update
if ($LASTEXITCODE -ne 0) { throw "Database update failed. Check SQL Server LocalDB and the connection string." }
Write-Host "5/5 Setup complete. Start Staybnb with: dotnet run" -ForegroundColor Green
