# Rubric traceability checklist

| Rubric category | Requirement mapping | Initial source status | Evidence needed before final submission |
|---|---|---|---|
| Authentication & role logic (20) | `AccountController`, `RoleSeeder`, `AdminController.ReviewApplication` | Implemented in source; not runtime tested | Register/login tests; prove Guest is removed after Host approval; direct authorization tests |
| Admin dashboard (15) | `AdminController.Index`, `Applications`, `Activity`, `PromoteGuests` | Implemented in source; not runtime tested | Dashboard screenshots; Admin vs SuperAdmin authorization tests; real database-backed statistics |
| Host dashboard (15) | `HostController` | Core source implemented; not runtime tested | Verify dashboard, property edit/activation, booking decisions and Host document verification |
| Host application & images (15) | `HostApplicationsController`, `SecureFileStorage` | Implemented in source; not runtime tested | Test required-image validation, file type/size checks, ownership, persistence and review flow |
| Booking & check-in (15) | `BookingService`, `BookingsController`, `CheckInController` | Partial pending test | Test overlap/concurrency, document ownership and verification completion, status transitions |
| Messaging & notifications (10) | `MessagesController`, `NotificationsController`, `NotificationService` | Implemented in source; not runtime tested | Test inbox privacy, persistent messages and read/unread state |
| Data & architecture (10) | `ApplicationDbContext`, `ActivityLogger`, entities | Data model defined; migration not generated | Generate migration, inspect schema/foreign keys/indexes, apply to LocalDB, test persistence |

The checklist distinguishes source implementation from verified runtime behaviour. Update a row only after evidence has been captured.