# Personal Finance Tracker API

A RESTful ASP.NET Core Web API for managing personal finance data, including accounts, categories, and transactions.

This project demonstrates building a traditional RESTful API using modern ASP.NET Core practices, including DTOs, service-layer architecture, Entity Framework Core, SQL Server, and centralized exception handling.

## Features

Current Features:
- Category CRUD operations
- DTO-based data transfer
- Service layer for business logic
- Entity Framework Core database access
- SQL Server integration
- Global exception handling using ASP.NET Core exception handlers
- Custom exceptions (NotFoundException, ConflictException)
- Scalar API documentation
- Dependency Injection

Planned Features:
- Account CRUD operations
- Transaction CRUD operations
- Advanced validation
- Authentication and authorization
- Clean Architecture
- Logging
- Deployment 

## Architecture

The API follows a layered approach:

- Controllers
  - Handle HTTP requests and responses

- DTOs
  - Control data exposed through the API

- Services
  - Contain business logic and database operations

- Entity Framework Core
  - Handles data persistence with SQL Server

- Exception Handling
  - Centralized error handling with consistent API responses

## Technologies

- C#
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Scalar OpenAPI Documentation
- REST API

## Getting Started

### Prerequisites

- .NET SDK
- SQL Server

### Installation

1. Clone the repository

2. Update the connection string in `appsettings.json`

3. Apply migrations:
   dotnet ef migrations add InitialMigration
   dotnet ef database update


5. Run the application:
  dotnet run


5. Open Scalar to explore the API endpoints

## API Endpoints

Example endpoints:

  - GET /api/categories
  
  - GET /api/categories/{id}
  
  - POST /api/categories
  
  - PUT /api/categories/{id}
  
  - DELETE /api/categories/{id}
  


## Future Improvements

- JWT authentication
- Automated testing
- Logging and monitoring
- Docker support
- Azure deployment





















A RESTful ASP.NET Core Web API for managing personal finance data.

## Features

- CRUD operations for:
  - Accounts
  - Categories
  - Transactions
- DTOs
- Service layer
- Global Exception Handling
- Entity Framework Core
- SQL Server
- Scalar API documentation
- Dependency Injection

## Technologies

- ASP.NET Core
- Entity Framework Core
- SQL Server
- C#
- Scalar
- REST API

## Getting Started

1. Clone the repository
2. Update the connection string in `appsettings.json`
3. Run the EF Core migrations
4. Start the application
5. Open Scalar to test the API
