# Architecture overview

- **Presentation:** MVC controllers and Razor views. State-changing form POSTs use anti-forgery validation; privileged endpoints use role authorization.
- **Identity:** ASP.NET Core Identity manages accounts and roles. Registration assigns Guest server-side; SuperAdmin role assignment is protected.
- **Persistence:** `ApplicationDbContext` maps Identity and assignment entities to SQL Server through EF Core. The initial migration and model snapshot are committed under `Data/Migrations/`.
- **Workflow services:** `BookingService` validates date, capacity, active-listing and application-level overlap rules; `BookingPricing` provides deterministic price breakdowns; `ActivityLogger`, `NotificationService`, and `SecureFileStorage` centralize common workflow logic.
- **Private files:** Guest identity documents are stored under `App_Data/private-documents`, outside `wwwroot`. Download actions check Host/property ownership.
- **State:** Host applications and bookings use `WorkflowStatus`; controllers enforce expected state transitions.

## Booking price
`TotalPrice = (PricePerNight × Nights) + CleaningFee + ServiceFee`
The booking stores a snapshot of nightly price and fees, so later property price changes do not rewrite an existing booking. Five automated unit tests validate the price-calculation logic in CI.

## Database setup
The initial EF Core migration was generated in CI and committed under `Data/Migrations/`. Apply it to the local SQL Server database using `dotnet ef database update`. CI proves the migration source builds and the project compiles; it does not confirm that a migration has been applied to the developer's LocalDB or that every database workflow succeeds at runtime.