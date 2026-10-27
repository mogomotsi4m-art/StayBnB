# Staybnb — Programming 742

Staybnb is an ASP.NET Core MVC property-rental application using ASP.NET Core Identity, Entity Framework Core, SQL Server LocalDB, Razor views, HTML, CSS and JavaScript. It targets .NET 10.

**Start here:** read [`LOCAL_START_HERE.md`](LOCAL_START_HERE.md) for local setup, SuperAdmin configuration, database migration, build, test and run commands.

## Prerequisites
- Windows with the .NET 10 SDK.
- SQL Server Express LocalDB instance `MSSQLLocalDB`.
- Internet access for the initial NuGet package restore.

## Main features in the current source
- Identity registration/login and role seeding for Guest, Host, Admin and SuperAdmin.
- Property discovery with city/type filters and property details.
- Host applications with property image uploads and Admin review.
- Booking requests, a stored price breakdown, and Host approve/reject workflow.
- Host-configurable check-in instructions and private guest-document upload/verification.
- Messaging, notifications, activity logging, Admin dashboard and SuperAdmin-only role promotion.
- Responsive CSS and Razor views.

## Price calculation
`TotalPrice = (PricePerNight × Nights) + CleaningFee + ServiceFee`

The booking stores a snapshot of the nightly rate and fees. `Staybnb.Tests/` contains deterministic unit tests for this calculation and its invalid-input handling.

## Source map
- `Controllers/` — MVC endpoints and authorization
- `Models/` — Identity and rental entities
- `Data/` — EF Core context, role seeding and migrations
- `Services/` — booking pricing, booking workflows, file storage, notifications and activity logging
- `ViewModels/` — validated form models
- `Views/` and `wwwroot/` — Razor UI and responsive styles/scripts
- `Staybnb.Tests/` — xUnit unit tests
- `docs/` — architecture, rubric mapping, verification status and test plan

## Verification status
The source has passed a Windows/.NET 10 CI build and the five booking-pricing unit tests passed in CI. The initial EF Core migration is included. The app still needs to be run locally against `MSSQLLocalDB` and manually tested through the full assignment workflows. `docs/TEST_PLAN.md` distinguishes CI checks from manual cases that remain untested.

## Security notes
No SuperAdmin password is committed. Use .NET user secrets. Keep identity documents, user data, local database files and private uploads out of the public source archive. Uploaded documents are stored outside `wwwroot`; review authorization and retention policies before any production deployment.

## Known limitations to resolve before final submission
Wishlist/review user interfaces, a comprehensive Admin document-review workflow, image deletion/reordering, email confirmation/reset, payment processing and real-time messaging are not integrated. Booking overlap validation is application-level and needs concurrency hardening. Verify the full check-in requirements against each property's required-document list.
