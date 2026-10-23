# Architecture overview

- **Presentation:** MVC controllers and Razor views. State-changing form POSTs use anti-forgery validation; privileged endpoints use role authorization.
- **Identity:** ASP.NET Core Identity manages accounts and roles. Registration assigns Guest server-side; SuperAdmin role assignment is protected.
- **Persistence:** `ApplicationDbContext` maps Identity and assignment entities to SQL Server through EF Core.
- **Workflow services:** `BookingService` calculates saved booking price snapshots and validates dates, capacity and active listing rules; `ActivityLogger`, `NotificationService`, and `SecureFileStorage` centralize common workflow logic.
- **Private files:** Guest identity documents are stored under `App_Data/private-documents`, outside `wwwroot`. Download actions check Host/property ownership.
- **State:** Host applications and bookings use `WorkflowStatus`; controllers enforce expected state transitions.

## Booking price
`TotalPrice = (PricePerNight × Nights) + CleaningFee + ServiceFee`
The booking stores a snapshot of nightly price and fees, so later property price changes do not rewrite an existing booking.

## Database startup
Generate and review an EF Core migration before the first verified SQL Server run. Use commands documented in the README. No generated migration or test result is claimed until it has been run and verified.