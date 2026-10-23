# Staybnb — Programming 742

Staybnb is an ASP.NET Core MVC property-rental application using ASP.NET Core Identity, Entity Framework Core, SQL Server LocalDB, Razor views, HTML, CSS and JavaScript. It targets .NET 10.

## Prerequisites
- Windows with the .NET 10 SDK.
- SQL Server LocalDB instance `MSSQLLocalDB`.
- Internet access for the first NuGet restore.

## Local setup
1. Clone this public repository and open PowerShell in the repository root:

   ```powershell
   git clone https://github.com/mogomotsi4m-art/StayBnB.git
   cd StayBnB
   ```

2. Configure a development-only SuperAdmin account using local user secrets. Run `dotnet user-secrets init` once if the project does not yet have a UserSecretsId:

   ```powershell
   dotnet user-secrets init
   dotnet user-secrets set "SuperAdmin:Email" "admin@example.local"
   dotnet user-secrets set "SuperAdmin:Password" "Replace-With-A-Unique-Strong-Password-123!"
   ```

   Use your own strong, unique development password; never commit it or include it in screenshots or reports.

3. Restore tools and dependencies, then create/update the local database using the migration already committed to this repository:

   ```powershell
   dotnet restore
   dotnet tool restore
   dotnet ef database update
   dotnet build --configuration Release
   dotnet test Staybnb.Tests/Staybnb.Tests.csproj --configuration Release
   dotnet run
   ```

4. Open the HTTPS URL printed by the application.

The project includes an EF Core design-time context factory and an initial migration in `Data/Migrations/`. Program startup also applies existing migrations before seeding roles. Review migrations before using this project with a database containing important data. LocalDB is for local development, not production hosting.

## Feature scope
- ASP.NET Core Identity registration and login; new users receive Guest server-side.
- Role seeding for Guest, Host, Admin and SuperAdmin. The initial SuperAdmin is created only when the password is provided through user secrets.
- Guest property discovery filtered by city and type; Host application form with mandatory property images; booking requests and stored price breakdowns; check-in instructions and private document upload; messaging and notifications.
- Admin Host-application approval/rejection, ActivityLog review, and SuperAdmin-only Guest-to-Admin promotion.
- Host property editing and activation, booking approval/rejection, check-in configuration, and private document review/download for the Host's own properties.
- Booking price snapshot: `(PricePerNight × Nights) + CleaningFee + ServiceFee`.
- Property images stored under `wwwroot/uploads/properties`. Identity documents are stored outside `wwwroot` under `App_Data/private-documents`; download actions enforce the Host/property ownership relationship.
- A Payment row records `NotProcessed`; there is no payment gateway or claim of real funds transfer.

## Repository map
- `Controllers/`: MVC endpoints and authorization
- `Models/`: Identity and rental entities
- `Data/`: EF Core context, role seeder, design-time factory, migrations
- `Services/`: booking pricing, booking workflows, file storage, notifications and activity logging
- `ViewModels/`: validated form models
- `Views/` and `wwwroot/`: Razor UI and responsive styles/scripts
- `Staybnb.Tests/`: deterministic unit tests for booking price calculations
- `docs/`: architecture, rubric traceability, test plan and report

## Testing and verification
GitHub Actions runs `dotnet restore`, builds in Release mode, executes the booking pricing unit tests, and retains the EF migration as an artifact. The CI build and migration-generation command have passed. This does **not** prove that the migration has been applied to your LocalDB or that all manual workflows have passed. After cloning, run the commands above and execute every scenario in `docs/TEST_PLAN.md`; update outcomes only with genuine results. Capture screenshots from the running application, not mockups.

## Security notes
Never commit passwords, user secrets, tokens, real identity documents, local database files or private application data. Uploaded documents and sample user records must not be added to this public repository. Property upload checks use filename extensions and limits; production deployment would also need robust file-content inspection and a retention policy. Review route authorization and state-transition behaviour before submission.

## Known limitations to address/test
Wishlist/review user interfaces, a comprehensive Admin document-review workflow, image deletion/reordering, email confirmation/reset, payment processing and real-time messaging are not integrated. Booking conflict validation is application-level and needs concurrency hardening for simultaneous requests. Verify the full check-in required-document logic against the property-specific list. Build success alone is not end-to-end verification.

## Assignment evidence
Use `docs/RUBRIC_TRACEABILITY.md` to map features to the rubric and `docs/TEST_PLAN.md` as an execution log. The public remote has genuine incremental commits; verify the latest history before final submission. Do not record unrun tests as passed.
