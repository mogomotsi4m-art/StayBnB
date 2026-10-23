# Build and verification status

- The development environment supplied by the developer reports .NET SDK 10.0.402 and ASP.NET Core runtime 10.0.13.
- SQL Server LocalDB instance name `MSSQLLocalDB` is present according to the developer.
- The authoring container has no `dotnet` executable, so `dotnet restore`, `dotnet build`, EF migration generation and runtime tests could not be run in this authoring environment.
- An automated GitHub Actions workflow is configured to restore and build against .NET 10. Check its run status before treating compilation as verified.
- The initial EF migration must be generated and tested on the Windows development machine.
- Do not represent this as a verified final submission until compilation, database creation, workflow tests, security checks and genuine screenshots are complete.