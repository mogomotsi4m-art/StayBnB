# Rubric traceability checklist

| Rubric category | Requirement mapping | Initial source status | Evidence needed before final submission |
|---|---|---|---|
| Authentication & role logic (20) | `AccountController`, `RoleSeeder`, `AdminController.ReviewApplication` | Source implemented; CI compiles | Register/login tests; prove Guest is removed after Host approval; direct authorization tests |
| Admin dashboard (15) | `AdminController.Index`, `Applications`, `Activity`, `PromoteGuests` | Source implemented; CI compiles | Dashboard screenshots; Admin vs SuperAdmin authorization tests; confirm live database-backed stats |
| Host dashboard (15) | `HostController` | Core source implemented; CI compiles | Verify property edit/activation, booking decisions and Host document verification against LocalDB |
| Host application & images (15) | `HostApplicationsController`, `SecureFileStorage` | Source implemented; CI compiles | Test required-image validation, file type/size checks, ownership, persistence and review flow |
| Booking & check-in (15) | `BookingService`, `BookingPricing`, `BookingsController`, `CheckInController` | Source implemented; deterministic price tests added; full workflow not tested | Confirm price unit tests pass in CI; test overlap/concurrency, document ownership and verification completion locally |
| Messaging & notifications (10) | `MessagesController`, `NotificationsController`, `NotificationService` | Source implemented; CI compiles | Test inbox privacy, persistent messages and read/unread state |
| Data & architecture (10) | `ApplicationDbContext`, `Data/Migrations`, `ActivityLogger` | Initial EF migration generated and committed; CI build passes | Apply migration to LocalDB, inspect tables/FKs/indexes and verify persistence/audit events |

The initial migration lives in `Data/Migrations/`. A passing CI build verifies compilation, not database integration or end-to-end correctness. Record manual results and attach authentic screenshots before submission.