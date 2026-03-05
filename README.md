# 🍽️ Clean Architecture Meal API

A **REST API** for managing meals with customizable option groups and items, built with **ASP.NET Core** on **.NET 10** following **Clean Architecture** principles.

## 📌 Overview

This project demonstrates a layered Clean Architecture approach to building a Web API. The domain models a **meal ordering system** where each meal can have multiple **option groups** (e.g., "Size", "Toppings"), and each group contains selectable **option items** (e.g., "Large", "Cheese").

### Recent Updates ✨

- **Mapster Integration** - High-performance object-to-object mapping (10-100x faster than AutoMapper)
- **New Specification Classes** - `AllMealsWithOptionsAdnOptionItems` for retrieving all meals with full projections
- **Enhanced Specification Pattern** - Selector-based specifications for direct DTO mapping

**Example structure:**
```
Meal: "Burger" ($10.00)
├── Option Group: "Size" (order: 1)
│   ├── Item: "Small"  ($0.00)
│   └── Item: "Large"  ($3.00) ⭐ popular
└── Option Group: "Extras" (order: 2)
    ├── Item: "Cheese" ($1.50)
    └── Item: "Bacon"  ($2.00)
```

## 🏗️ Architecture

The solution is organized into **4 layers**, each as a separate project, following the dependency rule where inner layers have no knowledge of outer layers.

```
┌──────────────────────────────────────┐
│           API (Presentation)         │  ← Controllers, Exception Handling, DI root
├──────────────────────────────────────┤
│            Application               │  ← Services, Contracts, Validation, Specifications
├──────────────────────────────────────┤
│           Infrastructure             │  ← EF Core, DbContext, Repositories, Configurations
├──────────────────────────────────────┤
│              Domain                  │  ← Entities (no dependencies)
└──────────────────────────────────────┘
```

### Domain (`CleanArchitucure.Domain`)

The innermost layer with **zero external dependencies**. Contains the core entity models:

| Entity | Description |
|---|---|
| `Meal` | A meal with name, description, and base price |
| `MealOptionGroup` | A customization category belonging to a meal (e.g., "Size") |
| `OptionGroupItems` | An individual option within a group (e.g., "Large") |

All entities use **GUID v7** string-based primary keys for chronological ordering.

### Application (`CleanArchitucure.Application`)

Contains all business logic and application rules. Has **no dependency** on infrastructure or presentation concerns.

- **Services** — `MealService`, `MealOptionsService`, `OptionItemsService` implement the business workflows
- **Contracts** — Request/Response records (`CreateMealRequest`, `MealResponse`, etc.)
- **Validators** — FluentValidation rules for all incoming requests with nested validation support
- **Specifications** — Generic Specification pattern for building type-safe queries:
  - `ISpecification<T>` - Base interface for query specifications
  - `ISpecification<T, TResult>` - Extended interface with projection selector
  - `MealByIdWithOptionsAndItemsSpec` - Single meal query with nested includes
  - `AllMealsWithOptionsAdnOptionItems` - All meals with full DTO projection
- **Mapping** — Mapster configuration for high-performance object mapping
- **Result Pattern** — `Result<T>` / `Error` abstractions for explicit success/failure handling without exceptions
- **Interfaces** — `IRepository<T>` and service interfaces defining contracts for the Infrastructure and API layers

### Infrastructure (`CleanArchitucure.Infrastructure`)

Implements persistence and data access concerns:

- **Entity Framework Core** with **SQL Server** provider
- **ApplicationDbContext** — Central DbContext with `DbSet` for all entities
- **Entity Configurations** — Fluent API configurations for column precision, max lengths, and relationships
- **Generic Repository** — `Repository<T>` implementing `IRepository<T>` with full specification support
- **EfSpecificationEvaluator** — Translates specification objects into EF Core `IQueryable` chains (filtering, includes, ordering, paging, tracking, query filters)
- **Migrations** — EF Core migrations for schema management
- **Delete Behavior** — All foreign keys default to `Restrict` to prevent accidental cascade deletes

### API (`CleanArchitcture.Api`)

The entry point and composition root:

- **Controllers** — `MealsController` with endpoints for creating and retrieving meals
- **Global Exception Handler** — `IExceptionHandler` implementation that catches unhandled exceptions and returns a standardized `ProblemDetails` response
- **Result Extensions** — `ToProblem()` extension that converts `Result` failures into RFC 7807 Problem Details responses
- **Dependency Injection** — Wires all layers together in `DependencyInjection.cs`

## 🔌 API Endpoints

| Method | Route | Description |
|---|---|---|
| `POST` | `/api/meals/add` | Create a new meal with option groups and items |
| `GET` | `/api/meals/{mealId}` | Get a meal by ID including all options and items |
| `GET` | `/api/meals` | Get all meals with full details |

### Create Meal — Request Body Example

```json
{
  "name": "Burger",
  "description": "Classic beef burger",
  "price": 10.00,
  "options": [
    {
      "name": "Size",
      "displayOrder": 1,
      "items": [
        { "name": "Small", "price": 0.00, "displayOrder": 1, "isPobular": false },
        { "name": "Large", "price": 3.00, "displayOrder": 2, "isPobular": true }
      ]
    }
  ]
}
```

### Get Meal — Response Example

```json
{
  "id": "0196119a-...",
  "name": "Burger",
  "description": "Classic beef burger",
  "price": 10.00,
  "options": [
    {
      "id": "0196119a-...",
      "name": "Size",
      "displayOrder": 1,
      "items": [
        { "id": "0196119a-...", "name": "Small", "price": 0.00, "displayOrder": 1, "isPobular": false },
        { "id": "0196119a-...", "name": "Large", "price": 3.00, "displayOrder": 2, "isPobular": true }
      ]
    }
  ]
}
```

## ✅ Validation Rules

Request validation is handled automatically via **FluentValidation** with **SharpGrip AutoValidation** (no manual validation calls needed in controllers).

| Field | Rules |
|---|---|
| Meal Name | Required, 2–30 characters |
| Meal Description | Required, 2–100 characters |
| Meal Price | Required, ≥ 0 |
| Option Group Name | Required, 2–30 characters |
| Option Group DisplayOrder | 1–25 |
| Option Groups | Unique names and display orders, max 25 |
| Option Item Name | Required, 2–30 characters |
| Option Item Price | ≥ 0 |
| Option Item DisplayOrder | 1–20 |
| Option Items (per group) | Unique names and display orders, max 2 |

## 🛠️ Tech Stack

- **.NET 10** / **C# 14**
- **ASP.NET Core** Web API
- **Entity Framework Core 10** (SQL Server)
- **FluentValidation 12** with auto-validation
- **Mapster** - High-performance object mapping
- **Specification Pattern** (custom implementation with projection support)
- **Result Pattern** for error handling

## ⚙️ Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- SQL Server (local or remote)

### Setup

1. **Clone the repository**
   ```bash
   git clone https://github.com/AbdelrahmanY25/clean-architecture-meal-api.git
   cd clean-architecture-meal-api
   ```

2. **Configure the connection string**

   Set the `DefaultConnection` in `CleanArchitcture/appsettings.json` or using User Secrets:
   ```bash
   dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=.;Database=MealsDb;Trusted_Connection=true;TrustServerCertificate=true;"
   ```

3. **Apply migrations**
   ```bash
   dotnet ef database update --project CleanArchitucure.Infrastructure --startup-project CleanArchitcture
   ```

4. **Run the application**
   ```bash
   dotnet run --project CleanArchitcture
   ```

5. **Explore the API** — Navigate to `https://localhost:<port>/openapi/v1.json` for the OpenAPI document.

## 📁 Project Structure

```
├── CleanArchitucure.Domain/              # Domain Layer
│   └── Entities/
│       ├── Meal.cs
│       ├── MealOptionGroup.cs
│       └── OptionGroupItems.cs
│
├── CleanArchitucure.Application/         # Application Layer
│   ├── Common/
│   │   ├── Abstractions/                 # Result, Error, Pagination
│   │   └── Errors/                       # Domain-specific error definitions
│   ├── Contracts/
│   │   ├── Meals/                        # Requests, Responses, Validators
│   │   ├── MealOptions/
│   │   └── OptionItems/
│   ├── Interfaces/
│   │   ├── Persistence/                  # IRepository<T>
│   │   └── Services/                     # IMealService, etc.
│   ├── Services/                         # Business logic implementations
│   ├── Specifications/                   # ISpecification, Specification base
│   │   └── MealsSpecifications/
│   └── DependencyInjection.cs
│
├── CleanArchitucure.Infrastructure/      # Infrastructure Layer
│   ├── Persistence/
│   │   ├── Configurations/               # EF Core entity configurations
│   │   ├── Migrations/
│   │   └── ApplicationDbContext.cs
│   ├── Repositories/                     # Generic Repository<T>
│   ├── Specifications/                   # EfSpecificationEvaluator
│   └── DependencyInjection.cs
│
└── CleanArchitcture/                     # API Layer (Presentation)
    ├── Controllers/                      # MealsController
    ├── Excceptions/                      # GlobalExcceptionHandler
    ├── Extensions/                       # ResultExtensions (ToProblem)
    ├── DependencyInjection.cs
    └── Program.cs
```

## 🔑 Key Design Decisions

- **Specification Pattern** — Encapsulates query logic (filters, includes, ordering, paging, projections) into reusable, testable objects rather than scattering LINQ across services.
- **Result Pattern** — Methods return `Result<T>` instead of throwing exceptions for expected failures, making error handling explicit and predictable.
- **Generic Repository** — A single `Repository<T>` handles all entities via `IRepository<T>`, reducing boilerplate while supporting full specification-driven queries.
- **Restrict Delete Behavior** — All foreign keys use `DeleteBehavior.Restrict` to prevent accidental cascade deletes across related entities.
- **Global Exception Handling** — Unhandled exceptions are caught by `IExceptionHandler` and returned as standardized RFC 7807 Problem Details.
- **Auto-Validation** — FluentValidation validators are automatically discovered and executed on incoming requests via SharpGrip, keeping controllers clean.

## 📄 License

This project is open source.
