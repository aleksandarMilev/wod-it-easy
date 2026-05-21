# Wod It Easy

A workout scheduling and participation platform for CrossFit athletes. Users register as athletes, create and manage workouts, and track their participation — all enforced by real domain business rules.

---

## Architecture

WodItEasy is built on **Domain-Driven Design (DDD)** and **Clean Architecture** principles. The backend is organized into four explicit layers with strict inward-only dependency flow:

```
WodItEasy.Domain          ← no external dependencies
      ↑
WodItEasy.Application     ← depends only on Domain
      ↑
WodItEasy.Infrastructure  ← implements Application abstractions
      ↑
WodItEasy.Web             ← HTTP entry point, delegates to Application
      ↑
WodItEasy.Startup         ← composition root, wires everything together
```

### Domain Layer

The heart of the application. It contains all business logic and has zero dependencies on frameworks or infrastructure.

- **Aggregate Roots:** `Athlete`, `Workout`, `Participation` — each owns its invariants and exposes only valid state transitions.
- **Domain Guards:** A static `Guard` class enforces string lengths, date ranges, URL formats, and more at object construction time. Invalid state cannot be created.
- **Business Rules encoded in entities:**
  - A workout is considered *closed* 2 hours before its start time — participants cannot join after that point.
  - A workout tracks its capacity and rejects joins when full.
  - Overlapping workout schedules are detected and prevented per athlete.
- **Domain Events:** Entities inherit from `Entity<TId>` which implements `IHaveDomainEvents`. Events are raised during state transitions and published via an EF Core interceptor at save time.
- **Value Objects & Enumerations:** `WorkoutType` is a type-safe enumeration (not a plain enum), and `ValueObject` provides structural equality for complex values.
- **Domain Exceptions:** `InvalidAthleteException`, `InvalidWorkoutException`, `WorkoutClosedException`, `WorkoutFullException` — thrown directly by entities, never by callers.

### Application Layer

Orchestrates use cases without knowing about HTTP or databases.

- **CQRS via MediatR:** All operations are expressed as Commands or Queries dispatched through a MediatR pipeline. There is no shared service class.
- **Validation Pipeline:** A `RequestValidationBehavior<TRequest, TResponse>` MediatR behavior runs FluentValidation on every request before the handler executes.
- **Repository Abstractions:** `IWorkoutRepository`, `IAthleteRepository`, `IParticipationRepository` are defined here — the Application layer never references EF Core.
- **AutoMapper with `IMapFrom<T>`:** Mapping is convention-based. Any output model that implements `IMapFrom<TSource>` is automatically registered by a scanning `MappingProfile`.
- **Paginated Results:** `PaginatedOutputModel<T>` is returned from search queries.

### Infrastructure Layer

Implements persistence and identity, all behind Application-defined interfaces.

- **EF Core 9 with SQL Server:** Fluent entity configurations keep the `DbContext` clean.
- **Generic + Specialized Repositories:** `DataRepository<TEntity>` handles save operations; `WorkoutRepository`, `AthleteRepository`, and `ParticipationRepository` add domain-specific queries.
- **Domain Event Interceptor:** `PublishDomainEventInterceptor` hooks into `SaveChangesAsync` to dispatch domain events after persistence.
- **ASP.NET Core Identity + JWT:** User management and token issuance are fully isolated to this layer.

### Web / Presentation Layer

Thin HTTP adapters — no business logic lives here.

- **`ApiController` base class** wires MediatR `Send()` into a reusable `Send<TResult>()` helper that maps results to `IActionResult`.
- **Result Pattern:** Handlers return `Result<T>`; extension methods convert it to the appropriate HTTP response.
- **Validation error middleware** catches `ValidationException` and returns structured 400 responses.

---

## Tech Stack

| Layer | Technology |
|---|---|
| Backend runtime | .NET 8 / ASP.NET Core |
| Domain orchestration | MediatR (CQRS) |
| Validation | FluentValidation |
| ORM | Entity Framework Core 9 |
| Database | SQL Server 2022 |
| Authentication | ASP.NET Core Identity + JWT Bearer |
| Object mapping | AutoMapper |
| API docs | Swagger / Swashbuckle |
| Frontend | React 18 + Vite |
| UI | React Bootstrap |
| Forms | React Hook Form + Yup |
| Routing | React Router v7 |
| HTTP client | Axios |
| Containerization | Docker + Docker Compose |
| Testing | xUnit + FakeItEasy + FluentAssertions |

---

## Features

- **Athlete profiles** — registered users create and manage their athlete identity.
- **Workout management** — create, update, and delete workouts with type, capacity, and schedule.
- **Participation** — join, cancel, and re-join workouts; the domain enforces all eligibility rules.
- **Workout search** — paginated search filtered by date.
- **Role-based access** — Admin and Athlete roles seeded on startup.
- **Real-time business rule enforcement** — closed workouts, full workouts, and schedule conflicts are rejected at the domain level, not the controller level.

---

## Project Structure

```
wod-it-easy/
├── server/
│   ├── WodItEasy.Domain/           # Entities, value objects, domain events, guards
│   ├── WodItEasy.Application/      # CQRS handlers, validators, DTOs, repo interfaces
│   ├── WodItEasy.Infrastructure/   # EF Core, repositories, identity, migrations
│   ├── WodItEasy.Web/              # Controllers, middleware, result mapping
│   └── WodItEasy.Startup/          # Composition root, DI wiring, seeding
└── client/
    └── src/
        ├── api/                    # Axios API clients per domain
        ├── components/             # Feature-grouped React components
        ├── contexts/               # UserContext, MessageContext
        └── hooks/                  # Custom React hooks
```

---

## Running Locally

### With Docker (recommended)

```bash
docker-compose up --build
```

This starts SQL Server, the .NET API, and the React frontend.

### Without Docker

**Backend:**
```bash
cd server/WodItEasy.Startup
dotnet run
```

Update `appsettings.Development.json` with your local SQL Server connection string, then apply migrations:
```bash
dotnet ef database update --project ../WodItEasy.Infrastructure
```

**Frontend:**
```bash
cd client
npm install
npm run dev
```

---

## Testing

```bash
cd server
dotnet test
```

Tests cover application handlers and domain logic using xUnit, FakeItEasy for mocks, and FluentAssertions for readable assertions.
