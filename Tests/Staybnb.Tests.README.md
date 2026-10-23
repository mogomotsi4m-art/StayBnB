# Automated tests — follow-up

There is not yet a dedicated automated test project in this repository. After the first restore/build and EF migration, create an xUnit project and add unit tests for booking dates, total-price calculation and role transitions, then integration tests against a disposable SQL Server database.

Recommended starting commands:

```powershell
dotnet new xunit -n Staybnb.Tests
dotnet add Staybnb.Tests reference Staybnb.csproj
dotnet test
```

Do not use the EF Core InMemory provider for tests that must verify SQL Server relational constraints. Record only tests actually executed.