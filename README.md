# Staybnb — Programming 742

Starter implementation of a property-rental web application using ASP.NET Core MVC, ASP.NET Core Identity, Entity Framework Core, and SQL Server LocalDB.

## Development prerequisites
- .NET 10 SDK
- SQL Server LocalDB instance `MSSQLLocalDB`
- Internet access for first package restore

## Local setup
1. Clone this repository and enter the project folder.
2. Configure `SuperAdmin:Email` and `SuperAdmin:Password` using `dotnet user-secrets`.
3. Run `dotnet restore`.
4. Install/update EF tool with `dotnet tool update --global dotnet-ef`.
5. Generate/apply migration: `dotnet ef migrations add InitialCreate`, then `dotnet ef database update`.
6. Run `dotnet run`.

Never commit credentials, user secrets, uploaded documents, or private personal data. Read the project's README and docs for workflow details, known limitations, rubric mapping and test plan. Migrations and test results must be generated and verified on a configured machine; do not treat the source as verified until build and runtime tests pass.
