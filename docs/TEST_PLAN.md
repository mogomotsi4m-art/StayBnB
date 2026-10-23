# Staybnb test plan and results

**Status at initial repository setup:** Not executed. Build, migration and end-to-end tests still need to be run on a configured Windows machine with LocalDB.

| ID | Test | Expected result | Actual result | Status |
|---|---|---|---|---|
| AUTH-01 | Register a new user | Account is created with Guest role | Not run | Not run |
| AUTH-02 | Register duplicate email | Registration rejected with validation | Not run | Not run |
| AUTH-03 | Guest requests Admin/Host endpoint directly | Access denied | Not run | Not run |
| ROLE-01 | Admin approves a valid Host application | Host added, Guest removed, property and notification/log saved | Not run | Not run |
| ROLE-02 | Admin attempts to promote a Guest | Access denied | Not run | Not run |
| ROLE-03 | SuperAdmin promotes Guest | Admin added and Guest removed | Not run | Not run |
| APP-01 | Submit application without image | Server rejects submission | Not run | Not run |
| APP-02 | Upload unsupported/oversized image | Rejected by server validation | Not run | Not run |
| APP-03 | Submit valid application | Property, image, application, log and notification persist | Not run | Not run |
| BOOK-01 | Check-out is before or equal to check-in | Booking rejected | Not run | Not run |
| BOOK-02 | Book inactive/unapproved property | Booking rejected | Not run | Not run |
| BOOK-03 | Request overlapping approved dates | Booking rejected | Not run | Not run |
| BOOK-04 | Valid booking | Correct price breakdown, Pending state and Payment row persist | Not run | Not run |
| BOOK-05 | Host approves pending booking | Approved status, notification and audit record | Not run | Not run |
| CHECK-01 | Guest uploads identity document | File outside web root; database record persists | Not run | Not run |
| CHECK-02 | Other user tries to download a document | Not found/denied | Not run | Not run |
| CHECK-03 | Host verifies required documents | Check-in advances only when rules are satisfied | Not run | Not run |
| MSG-01 | Send message | Message and recipient notification persist | Not run | Not run |
| MSG-02 | Request unrelated messages | No unrelated messages returned | Not run | Not run |
| NOT-01 | Mark notification read | Only the owner notification is updated | Not run | Not run |
| DB-01 | Restart after creating records | Records persist in SQL Server | Not run | Not run |
| BUILD-01 | Run restore and build | No errors | Not run | Not run |
| DB-02 | Apply EF migration | Migration applies cleanly | Not run | Not run |

## Execution commands

```powershell
dotnet restore
dotnet build
dotnet tool restore
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
```

Capture genuine screenshots after running the app and store them in `docs/evidence/`. Do not present conceptual UI images as evidence of working features.