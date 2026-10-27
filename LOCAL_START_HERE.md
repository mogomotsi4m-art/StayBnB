# Staybnb — local project setup

This folder is the local source project. It targets .NET 10 and uses SQL Server LocalDB with the instance name `MSSQLLocalDB`.

## 1. Open the project

1. Extract the complete project folder to a writable location, for example `C:\Projects\Staybnb`.
2. Open `Staybnb.csproj` in Visual Studio 2026 / an IDE supporting .NET 10, or open PowerShell in the project root.
3. Confirm the .NET 10 SDK and SQL Server Express LocalDB are installed:

   ```powershell
   dotnet --info
   sqllocaldb info MSSQLLocalDB
   ```

## 2. Configure a development SuperAdmin safely

The project does not contain a SuperAdmin password. Configure it using .NET user secrets, not by adding a password to `appsettings.json`:

```powershell
dotnet user-secrets init
dotnet user-secrets set "SuperAdmin:Email" "superadmin@staybnb.local"
dotnet user-secrets set "SuperAdmin:Password" "Use-A-Unique-Strong-Password-123!"
```

Replace the example password with your own strong development-only password. Never commit it or include it in screenshots or the report. If `UserSecretsId` is already present in the project, do not run `user-secrets init` again.

## 3. Restore, migrate, build and run

```powershell
dotnet restore
dotnet tool restore
dotnet ef database update
dotnet build --configuration Release
dotnet test Staybnb.Tests/Staybnb.Tests.csproj --configuration Release
dotnet run
```

The initial EF Core migration is included under `Data/Migrations/`. Startup applies existing migrations and seeds roles. `dotnet ef database update` creates/updates the `StaybnbDb` database in LocalDB. If migration application fails, check that the LocalDB instance is installed and running, then read the full exception before retrying.

Open the HTTPS address shown by `dotnet run` in your browser. To stop the server, press `Ctrl+C` in the terminal.

## 4. Before assignment submission

- Run every relevant scenario in `docs/TEST_PLAN.md` and write down actual outcomes.
- Capture genuine screenshots of the running application.
- Verify role transitions, bookings, document privacy, and database persistence locally.
- Keep identity documents and private uploads out of the source archive and Git.
- Review `docs/RUBRIC_TRACEABILITY.md` and complete the outstanding items.
- Do not describe a test as passed unless you ran it and observed the result.

## Current verification boundary

The source was compiled by the repository's Windows/.NET 10 GitHub Actions build, and the five booking-pricing unit tests passed in CI. That is useful baseline evidence, but it does not replace running this project on your own Windows machine, applying the migration to your LocalDB, and testing the full workflows locally.
