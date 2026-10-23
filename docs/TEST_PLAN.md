# Staybnb test plan and results

This document separates CI checks from local integration and end-to-end testing. Run the local cases against the developer’s configured SQL Server LocalDB and record evidence before final submission.

| ID | Test | Expected result | Actual result | Status |
|---|---|---|---|---|
| BUILD-01 | GitHub Actions restore and Release build | Dependencies restore and source compiles | Passed on Windows/.NET 10 in [run 39930209770](https://github.com/mogomotsi4m-art/StayBnB/actions/runs/39930209770) | Passed in CI |
| MIG-01 | Generate EF Core InitialCreate migration | Migration source and model snapshot are generated | Generated in CI and committed under `Data/Migrations/` | Passed in CI |
| PRICE-01 | Booking pricing unit tests | Correct nights, subtotal, one-time fees and total | Run via `dotnet test Staybnb.Tests/Staybnb.Tests.csproj`; check latest CI test job | Pending CI verification |
| AUTH-01 | Register a new user | Account is created with Guest role only | Not run manually | Not run |
| AUTH-02 | Register duplicate email | Registration rejected with validation | Not run | Not run |
| AUTH-03 | Guest requests Admin/Host endpoint directly | Access denied | Not run | Not run |
| ROLE-01 | Admin approves valid Host application | Host role added, Guest removed, property activated, notification/log created | Not run | Not run |
| ROLE-02 | Admin attempts to promote Guest | Access denied | Not run | Not run |
| ROLE-03 | SuperAdmin promotes Guest | Admin added and Guest removed | Not run | Not run |
| APP-01 | Submit application without image | Server rejects submission | Not run | Not run |
| APP-02 | Upload unsupported/oversized image | Rejected by server validation | Not run | Not run |
| APP-03 | Submit valid application | Property, image, application, log and notification persist | Not run | Not run |
| BOOK-01 | Check-out <= check-in | Booking rejected | Not run | Not run |
| BOOK-02 | Book inactive/unapproved property | Booking rejected | Not run | Not run |
| BOOK-03 | Request overlapping approved dates | Booking rejected | Not run | Not run |
| BOOK-04 | Valid booking | Correct nightly subtotal + fees, Pending state and Payment row persist | Not run against SQL Server | Not run |
| BOOK-05 | Host approves pending booking | Approved status, notification and audit record | Not run | Not run |
| CHECK-01 | Guest uploads identity document | File stored outside web root and record persists | Not run | Not run |
| CHECK-02 | Other user tries to access document download | Not found/denied | Not run | Not run |
| CHECK-03 | Host verifies required documents | Check-in advances only when requirements are satisfied | Not run | Not run |
| MSG-01 | Send message | Message and recipient notification persist | Not run | Not run |
| MSG-02 | User requests unrelated messages | No unrelated messages returned | Not run | Not run |
| NOT-01 | Mark notification read | Only owner notification is updated | Not run | Not run |
| DB-01 | Apply migration and restart after creating records | Migration applies; records persist in LocalDB | Not run locally | Not run |
| SEC-01 | Attempt cross-user property/message/document access | Request denied or not found | Not run manually | Not run |

## Local verification commands

```powershell
dotnet restore
dotnet tool restore
dotnet build --configuration Release
dotnet test Staybnb.Tests/Staybnb.Tests.csproj --configuration Release
dotnet ef database update
dotnet run
```

Capture genuine screenshots after running the app and store them in `docs/evidence/`. Do not use conceptual UI images as evidence of working features. Report only results actually observed.