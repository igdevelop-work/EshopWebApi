# WebAPI Application for Eshop Products

This project is a REST API for managing an eshop's products. The API provides endpoints for listing, creating, updating, and deleting products, as well as supporting pagination for product listings.

## Project Goals

- Provide REST API for eshop product management.
- Use proper layered architecture and apply SOLID design principles.
- Create Swagger documentation for all API endpoints.
- Implement unit tests covering API functionality.
- Support both real database (PostgreSQL) and mock data (Redis) for testing.

## Features

- Retrieve all products or a single product by ID.
- Support for paginated product listings.
- Create, update products.
- Switch between real database and mock data storage using environment variables.
- Swagger API documentation for easy testing and exploration of endpoints.

## Technologies Used

- **ASP.NET Core** for building the Web API.
- **PostgreSQL** as the real database.
- **Redis** for mock data storage.
- **Entity Framework Core** for database access.
- **Swagger** for API documentation.
- **Moq** for mocking services in unit tests.
- **XUnit** for unit testing.

## Prerequisites

- .NET 6 SDK or later
- PostgreSQL (if using real database)
- Redis (if using mock data for testing)

## Getting Started

### 1. Clone the repository

Run the following command to clone the repository and navigate into the project directory:

git clone https://github.com/igdevelop-work/EshopWebApi.git  
cd EshopWebApi

### 2. Configure environment variables

Create a `.env` file in the root of the project to configure database and Redis connections. Example:

DB_HOST=localhost  
DB_NAME=EshopDb  
DB_USER=postgres  
DB_PASSWORD=postgres  
REDIS_HOST=localhost  
REDIS_PORT=6379

Alternatively, you can configure these values directly in `appsettings.json`.

### 3. Install dependencies

To restore all necessary dependencies, run the following command:

```bash
  dotnet restore
```

### 4. Running the application

- **Run with real database (PostgreSQL):**
```bash
  dotnet run
```
- **Run with mock data (Redis):**
```bash
  dotnet run moc-test
```
The application will be available at `http://localhost:5000`.

### 5. Accessing API documentation

Swagger is available to explore the API and test the endpoints. After running the application, go to:

http://localhost:5000/swagger

### 6. Running tests

To run unit tests, use the following command:
```bash
cd tests
```
```bash
dotnet test
```
## API Endpoints

- `GET /api/products` - Retrieve all products
- `GET /api/products/{id}` - Retrieve a product by ID
- `POST /api/products` - Create a new product
- `PATCH /api/products/{id}` - Update an existing product
- `GET /api/products/v2` - Retrieve products with pagination (version 2)

## Project Structure

.
├── backend  
│   ├── interfaces  
│   ├── modules  
│   ├── shared  
│   └── infrastructure  
├── tests  
│   └── controllers  
├── Program.cs  
├── appsettings.json  
└── README.md  
