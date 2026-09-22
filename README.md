# Personal Finance Tracker API

A RESTful ASP.NET Core Web API for managing personal finance data, including accounts, account types, categories, transactions, and transaction types.

This project demonstrates building a traditional controller-based REST API using ASP.NET Core, Entity Framework Core, DTOs, dependency injection, service-layer architecture, repository patterns, query and command services, and centralized exception handling.

## Features

### CRUD Operations

* Account management
* Account Type management
* Category management
* Transaction management
* Transaction Type management

### API Architecture

* Traditional ASP.NET Core Controllers
* DTO-based data transfer
* Application and Infrastructure project separation
* Repository pattern
* Separate query and command services
* Dependency Injection
* Entity Framework Core
* SQL Server
* Entity relationships and foreign-key validation
* Asynchronous database operations

### Querying, Filtering, Sorting, and Pagination

Accounts support:

* Filtering by account type
* Filtering by active/inactive status
* Searching by account name

Transactions support:

* Searching transaction descriptions
* Filtering by account
* Filtering by category
* Filtering by transaction type
* Filtering by minimum and maximum amount
* Filtering by date range
* Sorting by supported fields
* Ascending and descending sort direction
* Pagination
* Configurable page size and page number
* Total result count and total page information

### Error Handling

* Global exception handling using ASP.NET Core `IExceptionHandler`
* Custom `NotFoundException`
* Custom `ConflictException`
* Centralized error responses

### API Documentation

* OpenAPI documentation
* Scalar API reference
* Endpoint testing through Scalar

## Architecture

The project uses a layered architecture with responsibilities separated between the API, Application, and Infrastructure projects.

### API

The API project contains the HTTP-facing portion of the application.

* Controllers handle HTTP requests and responses
* Dependency injection registers application services and repositories
* API-specific configuration is kept separate from business logic

### Application

The Application project contains the application's business and application logic.

* DTOs define data sent to and returned from the API
* Query services handle read operations
* Command services handle create, update, and deactivate operations
* Repository interfaces define the data-access contracts
* Query parameters and pagination models are defined here
* Business validation is handled outside the controllers

### Infrastructure

The Infrastructure project contains the implementation of database access.

* Entity Framework Core
* `FinanceDbContext`
* Repository implementations
* Database queries
* Entity Framework Core migrations
* SQL Server persistence

The repository layer keeps the `DbContext` out of the controllers and application services.

### Domain

The Domain project contains the application's core entities and relationships.

Examples include:

* `Account`
* `AccountType`
* `Category`
* `Transaction`
* `TransactionType`

## Application Flow

A typical request follows this general flow:

```text
HTTP Request
     ↓
Controller
     ↓
Application Service
     ↓
Repository Interface
     ↓
Repository Implementation
     ↓
Entity Framework Core
     ↓
SQL Server
```

For query operations, the query service also maps the returned domain entities into DTOs before they are returned by the API.

## Database Relationships

The application uses Entity Framework Core relationships between:

* Accounts and Account Types
* Transactions and Accounts
* Transactions and Categories
* Transactions and Transaction Types

Transactions can return related information such as:

* Account name
* Account type
* Category
* Transaction type

## Technologies

* C#
* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* Scalar
* OpenAPI
* REST API
* LINQ
* Dependency Injection
* Repository Pattern
* DTOs

## API Documentation

The API is documented and tested using Scalar.

### Scalar API Reference

![Scalar API Reference](images/allcrudoperations.png)

### Example API Response

![Transaction API Response](images/createtransaction.png)

![All Transaction APIs](images/alltransactions.png)

## Getting Started

### Prerequisites

* .NET SDK
* SQL Server
* Visual Studio or another .NET-compatible IDE

### Installation

1. Clone the repository.

2. Update the connection string in `appsettings.json`.

3. Apply the Entity Framework Core migrations:

```bash
dotnet ef database update
```

4. Run the application:

```bash
dotnet run
```

5. Open Scalar to explore and test the API endpoints.

## API Endpoints

### Accounts

```text
GET    /api/accounts
GET    /api/accounts/{id}
POST   /api/accounts
PUT    /api/accounts/{id}
DELETE /api/accounts/{id}
```

#### Account Query Parameters

The account GET endpoint supports filtering and searching.

Example:

```text
GET /api/accounts?accountTypeName=Checking&isActive=true&accountName=Primary
```

| Parameter         | Purpose                                    |
| ----------------- | ------------------------------------------ |
| `accountTypeName` | Filters accounts by account type           |
| `isActive`        | Filters accounts by active/inactive status |
| `accountName`     | Searches or filters by account name        |

### Account Types

```text
GET    /api/accounttypes
GET    /api/accounttypes/{id}
POST   /api/accounttypes
PUT    /api/accounttypes/{id}
DELETE /api/accounttypes/{id}
```

### Categories

```text
GET    /api/categories
GET    /api/categories/{id}
POST   /api/categories
PUT    /api/categories/{id}
DELETE /api/categories/{id}
```

### Transactions

```text
GET    /api/transactions
GET    /api/transactions/{id}
POST   /api/transactions
PUT    /api/transactions/{id}
DELETE /api/transactions/{id}
```

#### Transaction Query Parameters

The transaction GET endpoint supports searching, filtering, sorting, and pagination.

Example:

```text
GET /api/transactions?description=rent&minAmount=500&startDate=2026-01-01&endDate=2026-03-31&sortBy=amount&sortDirection=desc&pageNumber=1&pageSize=10
```

| Parameter             | Purpose                                    |
| --------------------- | ------------------------------------------ |
| `description`         | Searches transaction descriptions          |
| `accountId`           | Filters transactions by account            |
| `categoryName`        | Filters transactions by category name      |
| `transactionTypeName` | Filters transactions by transaction type   |
| `minAmount`           | Filters transactions at or above an amount |
| `maxAmount`           | Filters transactions at or below an amount |
| `startDate`           | Filters transactions from a date           |
| `endDate`             | Filters transactions through a date        |
| `sortBy`              | Determines the field used for sorting      |
| `sortDirection`       | Sorts ascending or descending              |
| `pageNumber`          | Selects the page of results                |
| `pageSize`            | Controls the number of results per page    |

### Transaction Types

```text
GET    /api/transactiontypes
GET    /api/transactiontypes/{id}
POST   /api/transactiontypes
PUT    /api/transactiontypes/{id}
DELETE /api/transactiontypes/{id}
```

## Current Development

The project is currently being refactored toward a cleaner separation of responsibilities using:

* Application services
* Command/query separation
* Repository interfaces
* Infrastructure repository implementations
* DTO mapping
* Entity Framework Core for persistence

Additional features such as authentication and authorization will be added as the project continues to develop.

## Future Improvements

* ASP.NET Core Identity
* JWT authentication and authorization
* FluentValidation
* Automated unit testing
* Logging and monitoring
* Docker support
* Deployment
* Additional reporting and financial analysis

























