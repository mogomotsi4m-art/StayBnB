# Staybnb — Programming 742

Staybnb is an ASP.NET Core MVC property-rental application using ASP.NET Core Identity, Entity Framework Core, SQL Server LocalDB, Razor views, HTML, CSS and JavaScript. It is developed against the .NET 10 SDK.

## Requirements
- Windows with .NET 10 SDK
- SQL Server LocalDB instance `MSSQLLocalDB` (confirmed by the developer)
- Internet access for the initial NuGet package restore
- Git, if you intend to clone and commit changes locally

## Local setup
1. Clone this repository and open a terminal in the repository root.
2. Configure a development-only SuperAdmin account using user secrets:

   ```powershell
   dotnet user-secrets init
   dotnet user-secrets set "SuperAdmin:Email" "admin@example.local"
   dotnet user-secrets set "SuperAdmin:Password" "Replace-With-A-Unique-Strong-Password-123!"
   ```

3. Restore dependencies: `dotnet restore`.
4. Install/update EF CLI if needed: `dotnet tool update --global dotnet-ef`.
5. Generate the initial migration and create the local database:

   ```powershell
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

6. Run `dotnet run` and open the HTTPS URL printed by the application.

The application applies existing migrations on startup, but the first migration must be generated before the first run. Review migration SQL before using this project with any database that contains important data. Do not use production credentials in a development database.

## Initial feature scope
- Registration automatically assigns the Guest role; login/logout use ASP.NET Core Identity.
- Role seeding creates Guest, Host, Admin and SuperAdmin roles. A SuperAdmin account is created only when a password is configured through user secrets.
- Guests can browse active approved properties, filter by city and type, submit Host applications with property images, request bookings, review pricing, upload private check-in documents, send messages and manage notifications.
- Admins can review Host applications, promote approved applicants from Guest to Host, inspect activity logs and view document queue metadata. SuperAdmin-only functionality promotes eligible Guests to Admin.
- Hosts can edit their own property details, activate/deactivate approved listings, approve/reject booking requests, configure check-in instructions, and verify/download documents for their own bookings.
- Booking prices are saved as a snapshot using `(PricePerNight × Nights) + CleaningFee + ServiceFee`.
- Uploaded property images are saved under `wwwroot/uploads/properties`. Identity documents are stored outside `wwwroot` under `App_Data/private-documents`, and download requests check the host/property ownership relationship.
- The Payment entity records a `NotProcessed` status. No actual payment gateway is integrated.

## Repository map
`Controllers/` contains MVC endpoints; `Models/` contains Identity and rental entities; `Data/` contains the EF Core context and role seeder; `Services/` contains booking, file-storage, activity and notification services; `ViewModels/` contains input models; `Views/` contains the Razor UI; `wwwroot/` contains styles and scripts. `docs/` contains the architecture notes, rubric mapping, testing plan and report.

## Testing and verification
Run `dotnet restore`, `dotnet build`, generate/apply the EF migration, and exercise the scenarios in `docs/TEST_PLAN.md`. The source package was prepared in an environment without the .NET CLI, so no build, migration, or end-to-end test is claimed as passed. The generated migration, screenshots, real test outcomes, and any required fixes must be added after testing on the configured Windows machine. Check `docs/RUBRIC_TRACEABILITY.md` for evidence requirements and known gaps.

## Security
Never commit passwords, user secrets, tokens, real identity documents, local database files or private app data. Uploaded documents and sample user records must not be added to this public repository. Review authorization and state-transition behaviour before submission.

## Known gaps before final submission
This is an initial implementation snapshot rather than a verified final release. The initial feature set does not fully implement wishlist and review user interfaces, a comprehensive Admin document-review workflow, a full gallery/image deletion experience, or automated tests. Booking conflict checking is application-level and needs hardening for simultaneous requests. Verify all route authorization and workflows. Email confirmation/reset, payment processing and real-time messaging are not integrated.

## Grading/evidence
Use `docs/RUBRIC_TRACEABILITY.md` to map functionality to the assignment rubric and `docs/TEST_PLAN.md` as an execution log. Record only tests actually run and capture genuine screenshots from the running application. Do not present unrun tests as passed.