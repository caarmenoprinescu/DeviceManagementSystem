# Device Management System

A full-stack web application for tracking company-owned mobile devices, their details, locations, and assigned users.

**GitHub Repository:** https://github.com/caarmenoprinescu/DeviceManagementSystem

**Video Demo:** *(link to be added)*

---

## Tech Stack

| Layer | Technology |
|-------|-----------|
| Backend | C# / ASP.NET Web API (.NET 8) |
| Data Access | Dapper + MS SQL Server |
| Frontend | Angular 19 + Angular Material |
| Authentication | JWT + BCrypt |
| AI Integration | Google Gemini API |
| Testing | xUnit + WebApplicationFactory |
| Version Control | Git / GitHub |

---

## Project Structure

```
DeviceManagement/
  Database/                   ← SQL scripts (run in order)
  DeviceManagement.Api/       ← ASP.NET Web API
    Controllers/
    Services/
    Repositories/
    Models/
    DTOs/
    Data/
  DeviceManagement.Tests/     ← Integration tests (xUnit)
  client/                     ← Angular frontend
  DeviceManagement.sln
```

---

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [MS SQL Server](https://www.microsoft.com/en-us/sql-server) (local instance)
- [Node.js](https://nodejs.org/) v18+
- [Angular CLI](https://angular.dev/tools/cli): `npm install -g @angular/cli`

---

## Database Setup

Run the SQL scripts **in order** using SQL Server Management Studio, DataGrip, or any SQL client connected to your local MS SQL Server instance:

```
01_create_tables.sql                ← Creates the database and tables
02_seed_data.sql                    ← Populates with sample devices and users
03_alter_add_manufacturer.sql       ← Adds manufacturer column to Devices
04_update_existing_data.sql         ← Updates seed data with manufacturer values
05_add_unique_constraint_device.sql ← Adds unique constraint on device name
06_alter_users_add_auth.sql         ← Adds email and password_hash columns to Users
```

All scripts are idempotent — safe to run multiple times without side effects.

> **Note:** The seed users (John Doe, Jane Smith, Alex Pop) do not have passwords set and cannot be used to log in. Use the Register page to create a new account.

---

## Backend Setup

1. Navigate to the API project:
```bash
cd DeviceManagement.Api
```

2. Open `appsettings.json` and fill in your values:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=DeviceManagementSystem;User Id=sa;Password=YOUR_PASSWORD;TrustServerCertificate=True;"
  },
  "Jwt": {
    "Key": "your-secret-key-minimum-32-characters-long",
    "Issuer": "DeviceManagementApi",
    "Audience": "DeviceManagementClient"
  },
  "Gemini": {
    "ApiKey": "your-gemini-api-key",
    "Model": "gemini-2.5-flash"
  }
}
```

> **Gemini API Key:** Free to obtain at [aistudio.google.com](https://aistudio.google.com) → Get API Key.
> 
> **Note:** The requests on a free account are limited. The description might not be generated anytime. Alternatively "gemini-2.5-flash-lite" model could be used.

3. Run the API:
```bash
dotnet run
```

The API starts at `http://localhost:5197`.
Swagger UI is available at `http://localhost:5197/swagger`.

---

## Frontend Setup

1. Navigate to the client folder:
```bash
cd client
npm install
ng serve
```

2. The app will be available at `http://localhost:4200`.

> Make sure the backend is running before starting the frontend.

---

## Running Tests

```bash
cd DeviceManagement.Tests
dotnet test
```

20 integration tests covering full CRUD for Devices and Users, including validation and edge cases (not found, bad request, post-delete verification).

---

## Features

### Phase 1 — Backend API
- Full CRUD for Devices and Users via RESTful endpoints
- Layered architecture: Controllers → Services → Repositories with interfaces
- Dapper for data access with explicit SQL mapping
- Business logic validation (duplicate device name, not found handling)
- Idempotent SQL scripts for database setup and migrations

### Phase 2 — Angular UI
- Device list with assigned user display (Angular Material table)
- Device detail view
- Create and edit device via a shared reusable form component
- Delete device directly from the list
- Form validation — required fields enforced at both HTML and API level; duplicate name check on create

### Phase 3 — Authentication & Authorization
- User registration and login
- Passwords hashed with BCrypt
- JWT tokens issued on login/register, stored in localStorage
- HTTP interceptor attaches token to every outgoing request
- All API endpoints protected with `[Authorize]`
- Device assignment — authenticated users can assign any unassigned device to themselves
- Device unassignment — users can unassign devices previously assigned to themselves
- Assign button is disabled if the device is already assigned to another user

### Phase 4 — AI Integration
- Device Description Generator powered by Google Gemini
- "Generate Description" button in the device form
- Sends device specs to the API, which builds a prompt and calls Gemini
- Returns a concise, professional description populated directly into the form field

---

## API Endpoints

### Auth
| Method | Endpoint | Auth Required | Description |
|--------|----------|---------------|-------------|
| POST | /api/auth/register | No | Register a new user |
| POST | /api/auth/login | No | Login and receive JWT token |

### Devices
| Method | Endpoint | Auth Required | Description |
|--------|----------|---------------|-------------|
| GET | /api/devices | Yes | Get all devices |
| GET | /api/devices/{id} | Yes | Get device by ID |
| POST | /api/devices | Yes | Create a new device |
| PUT | /api/devices/{id} | Yes | Update a device |
| DELETE | /api/devices/{id} | Yes | Delete a device |
| POST | /api/devices/generate-description | No | Generate AI description |

### Users
| Method | Endpoint | Auth Required | Description |
|--------|----------|---------------|-------------|
| GET | /api/users | Yes | Get all users |
| GET | /api/users/{id} | Yes | Get user by ID |
| POST | /api/users | Yes | Create a new user |
| PUT | /api/users/{id} | Yes | Update a user |
| DELETE | /api/users/{id} | Yes | Delete a user |

---

## Design Decisions & Known Limitations

This project implements the functionality as specified in the assignment. The following notes reflect awareness of how certain aspects would differ in a production system:

**Role-based access control** — All authenticated users currently have full CRUD access. In a production system, only `admin` users would manage devices and users, while `user` accounts would be limited to assigning and unassigning devices. The `role` field is present in the data model and included in the JWT claims — the foundation for RBAC is in place.

**Device assignment endpoints** — Assignment is currently handled via a standard PUT on the device. Dedicated `POST /api/devices/{id}/assign` and `POST /api/devices/{id}/unassign` endpoints would provide cleaner REST semantics and stricter ownership validation at the API level.

**Global exception handling** — Error handling is currently done per-controller with try-catch blocks. A global middleware exception handler would centralize this and ensure consistent error response formats across all endpoints.

**Free-text search (bonus phase)** — Not implemented within the submission deadline. The implementation would involve a ranked search endpoint scoring matches across Name, Manufacturer, Processor, and RAM fields with weighted tokens.
